using Microsoft.AspNetCore.Mvc;

namespace Genres.Microservice.Controllers;

[ApiController]
[Route("genres")]
public class GenresController : ControllerBase
{
    private static readonly object Sync = new();
    private static readonly string FilePath = Path.Combine(
        AppContext.BaseDirectory, "data", "genres.txt");

    public record GenreRequest(string Name);

    private static List<string> Load()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
        if (!System.IO.File.Exists(FilePath))
            System.IO.File.WriteAllLines(FilePath, ["Фантастика", "Детектив", "Роман"]);

        return System.IO.File.ReadAllLines(FilePath)
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Distinct()
            .ToList();
    }

    private static void Save(IEnumerable<string> genres) =>
        System.IO.File.WriteAllLines(FilePath, genres);

    [HttpGet]
    public ActionResult<List<string>> Get()
    {
        lock (Sync) return Ok(Load());
    }

    [HttpPost]
    public IActionResult Post([FromBody] GenreRequest request)
    {
        var name = request.Name?.Trim();
        if (string.IsNullOrWhiteSpace(name)) return BadRequest("Название жанра не может быть пустым.");
        lock (Sync)
        {
            var genres = Load();
            if (genres.Contains(name)) return Conflict("Такой жанр уже существует.");
            genres.Add(name);
            Save(genres);
        }
        return StatusCode(StatusCodes.Status201Created, name);
    }

    [HttpPut("{name}")]
    public IActionResult Put(string name, [FromBody] GenreRequest request)
    {
        var newName = request.Name?.Trim();
        if (string.IsNullOrWhiteSpace(newName)) return BadRequest("Название жанра не может быть пустым.");
        lock (Sync)
        {
            var genres = Load();
            var index = genres.IndexOf(name);
            if (index == -1) return NotFound("Жанр не найден.");
            if (newName != name && genres.Contains(newName)) return Conflict("Такой жанр уже существует.");
            genres[index] = newName;
            Save(genres);
        }
        return NoContent();
    }

    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        lock (Sync)
        {
            var genres = Load();
            if (!genres.Remove(name)) return NotFound("Жанр не найден.");
            Save(genres);
        }
        return NoContent();
    }
}
