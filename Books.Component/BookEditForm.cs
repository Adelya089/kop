namespace Books.Component;
public partial class BookEditForm : Form
{
    private bool _changed; private bool _saved;
    public Book Result { get; private set; }
    public BookEditForm(Book? book = null)
    {
        InitializeComponent();
        comboGenre.Items.AddRange(BookRepository.LoadGenres().Cast<object>().ToArray());
        Result = book == null ? new Book() : new Book { Id = book.Id, Title = book.Title, Description = book.Description, Genre = book.Genre, Price = book.Price };
        if (book != null) { textTitle.Text = book.Title; textDescription.Text = book.Description; comboGenre.SelectedItem = book.Genre; numericPrice.Value = Math.Min(numericPrice.Maximum, book.Price); }
        textTitle.TextChanged += MarkChanged; textDescription.TextChanged += MarkChanged; comboGenre.SelectedIndexChanged += MarkChanged; numericPrice.ValueChanged += MarkChanged; FormClosing += OnClosing;
    }
    private void MarkChanged(object? s, EventArgs e) => _changed = true;
    private void Save_Click(object? s, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(textTitle.Text)) { MessageBox.Show("Введите название."); return; }
        if (comboGenre.SelectedItem == null) { MessageBox.Show("Выберите жанр."); return; }
        Result.Title = textTitle.Text.Trim(); Result.Description = textDescription.Text.Trim(); Result.Genre = comboGenre.SelectedItem.ToString()!; Result.Price = numericPrice.Value;
        _saved = true; DialogResult = DialogResult.OK; Close();
    }
    private void OnClosing(object? s, FormClosingEventArgs e)
    {
        if (!_saved && _changed && MessageBox.Show("Есть несохраненные изменения. Закрыть без сохранения?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true;
    }
}
