namespace GenreDirectory.Component;
public partial class GenreDirectoryControl : UserControl
{
    public GenreDirectoryControl() { InitializeComponent(); LoadGenres(); }
    private void LoadGenres() { dataGridView1.Rows.Clear(); foreach (var g in GenreStorage.Load()) dataGridView1.Rows.Add(g); }
    private void SaveGenres()
    {
        var list = new List<string>();
        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            if (row.IsNewRow) continue;
            var value = row.Cells[0].Value?.ToString()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(value)) { MessageBox.Show("Пустой жанр сохранять нельзя."); return; }
            list.Add(value);
        }
        GenreStorage.Save(list);
    }
    private void Grid_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Insert) { dataGridView1.Rows.Add(); e.Handled = true; }
        else if (e.KeyCode == Keys.Delete && dataGridView1.SelectedRows.Count > 0)
        {
            if (MessageBox.Show("Удалить выбранные жанры?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { foreach (DataGridViewRow r in dataGridView1.SelectedRows) if (!r.IsNewRow) dataGridView1.Rows.Remove(r); SaveGenres(); }
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.Enter) SaveGenres();
    }
    private void Grid_CellEndEdit(object? sender, DataGridViewCellEventArgs e) => SaveGenres();
}
