namespace Books.Component
{
    public partial class BooksControl : UserControl
    {
        private List<Book> _books = new();

        public BooksControl()
        {
            InitializeComponent();
            Reload();
        }

        private void Reload()
        {
            _books = BookRepository.Load();
            BuildTree();
        }

        private void BuildTree()
        {
            treeView1.Nodes.Clear();

            foreach (var genreGroup in _books.GroupBy(x => x.Genre).OrderBy(x => x.Key))
            {
                var genreNode = treeView1.Nodes.Add(genreGroup.Key);

                foreach (var priceGroup in genreGroup.GroupBy(x => x.Price).OrderBy(x => x.Key))
                {
                    var priceText = priceGroup.Key == 0 ? "Бесплатно" : $"{priceGroup.Key:0.00} руб.";
                    var priceNode = genreNode.Nodes.Add(priceText);

                    foreach (var book in priceGroup.OrderBy(x => x.Title))
                    {
                        var titleNode = priceNode.Nodes.Add(book.Title);
                        var idNode = titleNode.Nodes.Add($"ID: {book.Id}");
                        titleNode.Tag = book.Id;
                        idNode.Tag = book.Id;
                    }
                }
            }

            treeView1.ExpandAll();
        }

        private int? SelectedId()
        {
            var node = treeView1.SelectedNode;
            while (node != null)
            {
                if (node.Tag is int id) return id;
                node = node.Parent;
            }
            return null;
        }

        private void AddBook()
        {
            using var f = new BookEditForm();
            if (f.ShowDialog() != DialogResult.OK) return;
            _books = BookRepository.Load();
            f.Result.Id = _books.Count == 0 ? 1 : _books.Max(x => x.Id) + 1;
            _books.Add(f.Result);
            BookRepository.Save(_books);
            Reload();
        }

        private void EditBook()
        {
            var id = SelectedId();
            if (id == null) return;

            _books = BookRepository.Load();
            var b = _books.First(x => x.Id == id.Value);

            using var f = new BookEditForm(b);
            if (f.ShowDialog() != DialogResult.OK) return;

            b.Title = f.Result.Title;
            b.Description = f.Result.Description;
            b.Genre = f.Result.Genre;
            b.Price = f.Result.Price;

            BookRepository.Save(_books);
            Reload();
        }

        private void DeleteBook()
        {
            var id = SelectedId();
            if (id == null) return;

            _books = BookRepository.Load();
            var b = _books.First(x => x.Id == id.Value);

            if (MessageBox.Show($"Удалить книгу «{b.Title}»?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            _books.Remove(b);
            BookRepository.Save(_books);
            Reload();
        }

        private void Add_Click(object? sender, EventArgs e) => AddBook();

        private void Edit_Click(object? sender, EventArgs e) => EditBook();

        private void Delete_Click(object? sender, EventArgs e) => DeleteBook();

        private void Tree_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) AddBook();
            if (e.Control && e.KeyCode == Keys.U) EditBook();
            if (e.Control && e.KeyCode == Keys.D) DeleteBook();
        }
    }
}