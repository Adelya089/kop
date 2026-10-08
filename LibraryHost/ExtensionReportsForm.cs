using Books.Component;
using ComponentContracts;
using PluginsContracts;

namespace LibraryHost;

public partial class ExtensionReportsForm : Form
{
    private readonly List<IReportDocumentWithContextTextsContract> _textPlugins;
    private readonly List<IReportDocumentWithChartPieContract> _piePlugins;
    private readonly List<IReportDocumentWithTableColumnRowHeaderContract> _tablePlugins;

    public ExtensionReportsForm(string pluginDirectory)
    {
        InitializeComponent();

        _textPlugins = ReportPluginLoader.Load<IReportDocumentWithContextTextsContract>(pluginDirectory);
        _piePlugins = ReportPluginLoader.Load<IReportDocumentWithChartPieContract>(pluginDirectory);
        _tablePlugins = ReportPluginLoader.Load<IReportDocumentWithTableColumnRowHeaderContract>(pluginDirectory);

        FillCombo(comboText, _textPlugins);
        FillCombo(comboPie, _piePlugins);
        FillCombo(comboTable, _tablePlugins);
    }

    private static void FillCombo<T>(ComboBox combo, List<T> plugins) where T : IReportDocumentContract
    {
        combo.Items.Clear();
        foreach (var plugin in plugins)
            combo.Items.Add(plugin.DocumentFormat);

        if (combo.Items.Count > 0)
            combo.SelectedIndex = 0;
    }

    private async void ButtonText_Click(object? sender, EventArgs e)
    {
        if (comboText.SelectedIndex < 0)
        {
            MessageBox.Show("Не найден плагин для текстового отчета.");
            return;
        }

        var books = BookRepository.Load().Where(x => x.Price == 0).ToList();
        if (books.Count == 0)
        {
            MessageBox.Show("Нет бесплатных книг для отчета.");
            return;
        }

        var plugin = _textPlugins[comboText.SelectedIndex];
        var path = SelectFile(plugin.DocumentFormat, "Бесплатные книги");
        if (path == null) return;

        var paragraphs = books
            .Select(x => $"{x.Title}. {x.Description}")
            .ToList();

        await plugin.CreateDocumentAsync(path, "Бесплатные книги", paragraphs);
        MessageBox.Show("Отчет сформирован.");
    }

    private async void ButtonPie_Click(object? sender, EventArgs e)
    {
        if (comboPie.SelectedIndex < 0)
        {
            MessageBox.Show("Не найден плагин для круговой диаграммы.");
            return;
        }

        var data = BookRepository.Load()
            .Where(x => x.Price == 0)
            .GroupBy(x => x.Genre)
            .Select(x => (Parameter: x.Key, Value: (double)x.Count()))
            .ToList();

        if (data.Count == 0)
        {
            MessageBox.Show("Нет бесплатных книг для диаграммы.");
            return;
        }

        var plugin = _piePlugins[comboPie.SelectedIndex];
        var path = SelectFile(plugin.DocumentFormat, "Бесплатные книги по жанрам");
        if (path == null) return;

        await plugin.CreateDocumentAsync(
            path,
            "Отчет по бесплатным книгам",
            "Количество бесплатных книг по жанрам",
            "Книги",
            data);

        MessageBox.Show("Отчет сформирован.");
    }

    private async void ButtonTable_Click(object? sender, EventArgs e)
    {
        if (comboTable.SelectedIndex < 0)
        {
            MessageBox.Show("Не найден плагин для табличного отчета.");
            return;
        }

        var books = BookRepository.Load();
        if (books.Count == 0)
        {
            MessageBox.Show("Нет книг для отчета.");
            return;
        }

        var data = books.Select(x => new ReportBookRow
        {
            Id = x.Id,
            Title = x.Title,
            Genre = x.Genre,
            PriceText = x.Price == 0 ? "бесплатная" : $"{x.Price:0.00} руб."
        }).ToList();

        var plugin = _tablePlugins[comboTable.SelectedIndex];
        var path = SelectFile(plugin.DocumentFormat, "Все книги");
        if (path == null) return;

        await plugin.CreateDocumentAsync(
            path,
            "Информация по всем книгам",
            new List<int> { 1200, 3000, 2200, 2200 },
            Enumerable.Repeat(500, data.Count + 1).ToList(),
            true,
            new List<(string Header, string PropertyName, string FiledName)>
            {
                ("", nameof(ReportBookRow.Id), ""),
                ("Название", nameof(ReportBookRow.Title), ""),
                ("Жанр", nameof(ReportBookRow.Genre), ""),
                ("Стоимость", nameof(ReportBookRow.PriceText), "")
            },
            data);

        MessageBox.Show("Отчет сформирован.");
    }

    private static string? SelectFile(string format, string defaultName)
    {
        var extension = format.ToLowerInvariant() switch
        {
            "xlsx" or "excel" => "xlsx",
            "docx" or "word" => "docx",
            "pdf" => "pdf",
            _ => format.TrimStart('.')
        };

        var filter = extension switch
        {
            "xlsx" => "Excel (*.xlsx)|*.xlsx",
            "docx" => "Word (*.docx)|*.docx",
            "pdf" => "PDF (*.pdf)|*.pdf",
            _ => $"Файл (*.{extension})|*.{extension}"
        };

        using var dialog = new SaveFileDialog
        {
            Filter = filter,
            DefaultExt = extension,
            AddExtension = true,
            FileName = defaultName
        };

        return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
    }
}
