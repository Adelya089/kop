namespace GenreDirectory.Component;
partial class GenreDirectoryControl
{
    private DataGridView dataGridView1 = null!;
    private void InitializeComponent()
    {
        dataGridView1 = new DataGridView();
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView1.Columns.Add("Genre", "Жанр");
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.KeyDown += Grid_KeyDown;
        dataGridView1.CellEndEdit += Grid_CellEndEdit;
        Controls.Add(dataGridView1);
        Name = "GenreDirectoryControl"; Size = new Size(700, 400);
    }
}
