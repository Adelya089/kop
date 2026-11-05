using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Genres.Microservice.Controllers;

[ApiController]
[Route("genres")]
public sealed class GenresController(GenreDatabase database) : ControllerBase
{
    public sealed record GenreRequest(string? Name);

    [HttpGet]
    public async Task<ActionResult<List<string>>> Get()
    {
        var result = new List<string>();

        await using var connection = await database.OpenAsync();
        await using var command = new NpgsqlCommand("SELECT name FROM genres ORDER BY name", connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(reader.GetString(0));
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenreRequest request)
    {
        var name = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Название жанра обязательно.");
        }

        await using var connection = await database.OpenAsync();
        await using var command = new NpgsqlCommand(
            "INSERT INTO genres(name) VALUES (@name) ON CONFLICT DO NOTHING",
            connection);
        command.Parameters.AddWithValue("name", name);

        if (await command.ExecuteNonQueryAsync() == 0)
        {
            return Conflict("Такой жанр уже существует.");
        }

        return StatusCode(201, name);
    }

    [HttpPut("{name}")]
    public async Task<IActionResult> Put(string name, [FromBody] GenreRequest request)
    {
        var newName = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(newName))
        {
            return BadRequest("Название жанра обязательно.");
        }

        await using var connection = await database.OpenAsync();
        await using var command = new NpgsqlCommand(
            "UPDATE genres SET name=@newName WHERE name=@name",
            connection);
        command.Parameters.AddWithValue("newName", newName);
        command.Parameters.AddWithValue("name", name);

        try
        {
            if (await command.ExecuteNonQueryAsync() == 0)
            {
                await using var check = new NpgsqlCommand(
                    "SELECT EXISTS(SELECT 1 FROM genres WHERE name=@name)",
                    connection);
                check.Parameters.AddWithValue("name", name);

                return (bool)(await check.ExecuteScalarAsync() ?? false)
                    ? Conflict("Жанр используется книгами и не может быть переименован.")
                    : NotFound();
            }
        }
        catch (PostgresException ex) when (ex.SqlState is "23505" or "23503")
        {
            return Conflict("Название занято или жанр используется книгами.");
        }

        return NoContent();
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        await using var connection = await database.OpenAsync();
        await using var command = new NpgsqlCommand(
            "DELETE FROM genres WHERE name=@name",
            connection);
        command.Parameters.AddWithValue("name", name);

        try
        {
            if (await command.ExecuteNonQueryAsync() == 1)
            {
                return NoContent();
            }
        }
        catch (PostgresException ex) when (ex.SqlState == "23503")
        {
            return Conflict("Жанр используется книгами.");
        }

        await using var check = new NpgsqlCommand(
            "SELECT EXISTS(SELECT 1 FROM genres WHERE name=@name)",
            connection);
        check.Parameters.AddWithValue("name", name);

        return (bool)(await check.ExecuteScalarAsync() ?? false)
            ? Conflict("Жанр используется книгами.")
            : NotFound();
    }
}