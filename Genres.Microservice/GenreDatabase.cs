using Npgsql;

namespace Genres.Microservice;

public sealed class GenreDatabase(IConfiguration config)
{
    private readonly string _connection = config.GetConnectionString("Default")
        ?? throw new InvalidOperationException("Не задана строка подключения к БД.");

    public async Task<NpgsqlConnection> OpenAsync()
    {
        var connection = new NpgsqlConnection(_connection);
        await connection.OpenAsync();
        return connection;
    }

    public async Task InitializeAsync()
    {
        await using var connection = await OpenAsync();

        await using (var command = new NpgsqlCommand(
            "CREATE TABLE IF NOT EXISTS genres (name TEXT PRIMARY KEY)",
            connection))
        {
            await command.ExecuteNonQueryAsync();
        }

        await using var count = new NpgsqlCommand("SELECT COUNT(*) FROM genres", connection);

        if ((long)(await count.ExecuteScalarAsync() ?? 0L) == 0)
        {
            await using var seed = new NpgsqlCommand(
                "INSERT INTO genres(name) VALUES ('Фантастика'), ('Детектив'), ('Роман') ON CONFLICT DO NOTHING",
                connection);
            await seed.ExecuteNonQueryAsync();
        }
    }
}