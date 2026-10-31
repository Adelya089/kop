using Books.Microservice.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System.Net.Http.Json;
namespace Books.Microservice.Controllers;
[ApiController]
[Route("books")]
public sealed class BooksController(BookDatabase database, IHttpClientFactory httpClientFactory) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Book>>> Get() => Ok(await database.AllAsync());

    private static string? Validate(BookRequest value)
    {
        if (string.IsNullOrWhiteSpace(value.Title)) return "Название обязательно.";
        if (string.IsNullOrWhiteSpace(value.Description)) return "Описание обязательно.";
        if (string.IsNullOrWhiteSpace(value.Genre)) return "Жанр обязателен.";
        if (value.Price < 0) return "Стоимость не может быть отрицательной.";
        return null;
    }
    private async Task<bool> GenreExists(string genre)
    {
        var genres = await httpClientFactory.CreateClient("genres").GetFromJsonAsync<List<string>>("genres");
        return genres?.Contains(genre, StringComparer.Ordinal) == true;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BookRequest request)
    {
        var error = Validate(request);
        if (error != null) return BadRequest(error);
        var genre = request.Genre!.Trim();
        if (!await GenreExists(genre)) return BadRequest("Жанр отсутствует в первом микросервисе.");
        await using var connection = await database.OpenAsync();
        await using var command = new NpgsqlCommand("INSERT INTO books(title,description,genre,price) VALUES (@title,@description,@genre,@price) RETURNING id", connection);
        SetParameters(command, request, genre);
        try
        {
            var id = (int)(await command.ExecuteScalarAsync() ?? 0);
            return StatusCode(201, new { id });
        }
        catch (PostgresException ex) when (ex.SqlState == "23503") { return BadRequest("Жанр не найден."); }
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] BookRequest request)
    {
        var error = Validate(request);
        if (error != null) return BadRequest(error);
        var genre = request.Genre!.Trim();
        if (!await GenreExists(genre)) return BadRequest("Жанр отсутствует в первом микросервисе.");
        await using var connection = await database.OpenAsync();
        await using var command = new NpgsqlCommand("UPDATE books SET title=@title,description=@description,genre=@genre,price=@price WHERE id=@id", connection);
        SetParameters(command, request, genre);
        command.Parameters.AddWithValue("id", id);
        try { return await command.ExecuteNonQueryAsync() == 0 ? NotFound() : NoContent(); }
        catch (PostgresException ex) when (ex.SqlState == "23503") { return BadRequest("Жанр не найден."); }
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await using var connection = await database.OpenAsync();
        await using var command = new NpgsqlCommand("DELETE FROM books WHERE id=@id", connection);
        command.Parameters.AddWithValue("id", id);
        return await command.ExecuteNonQueryAsync() == 0 ? NotFound() : NoContent();
    }
    private static void SetParameters(NpgsqlCommand command, BookRequest item, string genre)
    {
        command.Parameters.AddWithValue("title", item.Title!.Trim());
        command.Parameters.AddWithValue("description", item.Description!.Trim());
        command.Parameters.AddWithValue("genre", genre);
        command.Parameters.Add(new NpgsqlParameter("price", NpgsqlDbType.Numeric)
            { Value = item.Price.HasValue ? (object)item.Price.Value : DBNull.Value });
    }
}
