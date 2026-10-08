using System.Text.Json;
namespace BookReport.Component;
public partial class BookReportControl : UserControl
{
    private List<Book> _books=new();
    public BookReportControl(){InitializeComponent();LoadData();}
    private void LoadData(){var dir=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Lab2LibraryVariant4");var bp=Path.Combine(dir,"books.json");var gp=Path.Combine(dir,"genres.txt");if(File.Exists(bp))_books=JsonSerializer.Deserialize<List<Book>>(File.ReadAllText(bp))??new();if(File.Exists(gp))comboGenre.Items.AddRange(File.ReadAllLines(gp).Where(x=>!string.IsNullOrWhiteSpace(x)).Distinct().Cast<object>().ToArray());}
    private void Build_Click(object? s, EventArgs e){if(comboGenre.SelectedItem==null){MessageBox.Show("Выберите жанр.");return;}var g=comboGenre.SelectedItem.ToString()!;dataGridView1.DataSource=null;dataGridView1.DataSource=_books.Where(x=>x.Genre==g).ToList();}
}
