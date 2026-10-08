namespace GenreDirectory.Component;
public static class GenreStorage
{
    public static string FilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lab2LibraryVariant4", "genres.txt");
    public static List<string> Load()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
        if (!File.Exists(FilePath)) File.WriteAllLines(FilePath, new[] { "Фантастика", "Детектив", "Роман" });
        return File.ReadAllLines(FilePath).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
    }
    public static void Save(IEnumerable<string> genres)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
        File.WriteAllLines(FilePath, genres.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct());
    }
}
