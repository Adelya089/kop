using Books.Component;

namespace BookReport.Component
{
    public partial class BookReportControl : UserControl
    {
        public BookReportControl()
        {
            InitializeComponent();
            LoadGenres();
        }

        private void LoadGenres()
        {
            comboGenre.Items.Clear();
            foreach (var genre in BookRepository.LoadGenres()) comboGenre.Items.Add(genre);
            if (comboGenre.Items.Count > 0) comboGenre.SelectedIndex = 0;
        }

        private void Build_Click(object? sender, EventArgs e)
        {
            if (comboGenre.SelectedItem == null)
            {
                MessageBox.Show("Выберите жанр.");
                return;
            }

            var genre = comboGenre.SelectedItem.ToString();

            var books = BookRepository.Load()
                .Where(x => x.Genre == genre)
                .Select(x => new
                {
                    x.Id,
                    Название = x.Title,
                    Описание = x.Description,
                    Жанр = x.Genre,
                    Стоимость = x.Price == 0 ? "Бесплатно" : $"{x.Price:0.00} руб."
                })
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = books;
        }
    }
}