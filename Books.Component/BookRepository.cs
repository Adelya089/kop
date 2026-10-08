namespace Books.Component
{
    public static class BookRepository
    {
        private static readonly List<Book> _books = new();

        private static string FolderPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lab2LibraryVariant4");
        private static string GenresFilePath => Path.Combine(FolderPath, "genres.txt");

        public static List<Book> Load()
        {
            return _books.ToList();
        }

        public static void Save(List<Book> books)
        {
            _books.Clear();
            _books.AddRange(books);
        }

        public static List<string> LoadGenres()
        {
            Directory.CreateDirectory(FolderPath);
            if (!File.Exists(GenresFilePath)) return new List<string>();
            return File.ReadAllLines(GenresFilePath).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
        }
    }
}