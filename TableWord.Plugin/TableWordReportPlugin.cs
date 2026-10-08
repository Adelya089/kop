using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PluginsContracts;

namespace TableWord.Plugin;

public sealed class TableWordReportPlugin : IReportDocumentWithTableColumnRowHeaderContract
{
    public string DocumentFormat => "docx";

    public Task CreateDocumentAsync<T>(
        string filePath,
        string header,
        List<int> columnsWidth,
        List<int> rowsHeights,
        bool isHeaderFirstRow,
        List<(string Header, string PropertyName, string FiledName)> headers,
        List<T> data)
    {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));
        if (string.IsNullOrWhiteSpace(header)) throw new ArgumentNullException(nameof(header));
        if (columnsWidth == null) throw new ArgumentNullException(nameof(columnsWidth));
        if (columnsWidth.Count == 0) throw new ArgumentOutOfRangeException(nameof(columnsWidth));
        if (rowsHeights == null) throw new ArgumentNullException(nameof(rowsHeights));
        if (rowsHeights.Count == 0) throw new ArgumentOutOfRangeException(nameof(rowsHeights));
        if (headers == null) throw new ArgumentNullException(nameof(headers));
        if (headers.Count == 0) throw new ArgumentOutOfRangeException(nameof(headers));
        if (data == null) throw new ArgumentNullException(nameof(data));
        if (data.Count == 0) throw new ArgumentOutOfRangeException(nameof(data));
        if (columnsWidth.Count != headers.Count) throw new ArgumentException("Количество ширин колонок должно совпадать с количеством заголовков.");

        return Task.Run(() =>
        {
            using var document = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document);
            var mainPart = document.AddMainDocumentPart();
            mainPart.Document = new Document();
            var body = mainPart.Document.AppendChild(new Body());

            body.Append(new Paragraph(new Run(new RunProperties(new Bold(), new FontSize { Val = "32" }), new Text(header))));
            body.Append(new Paragraph());

            var table = new Table();
            table.AppendChild(CreateBorders());

            if (isHeaderFirstRow)
            {
                var headerRow = new TableRow();
                for (var i = 0; i < headers.Count; i++)
                    headerRow.Append(CreateCell(headers[i].Header, columnsWidth[i], true));
                table.Append(headerRow);

                foreach (var item in data)
                {
                    var row = new TableRow();
                    for (var i = 0; i < headers.Count; i++)
                        row.Append(CreateCell(GetValue(item, headers[i]), columnsWidth[i], false));
                    table.Append(row);
                }
            }
            else
            {
                for (var i = 0; i < headers.Count; i++)
                {
                    var row = new TableRow();
                    row.Append(CreateCell(headers[i].Header, columnsWidth[0], true));
                    foreach (var item in data)
                        row.Append(CreateCell(GetValue(item, headers[i]), columnsWidth[Math.Min(i, columnsWidth.Count - 1)], false));
                    table.Append(row);
                }
            }

            body.Append(table);
            mainPart.Document.Save();
        });
    }

    private static TableProperties CreateBorders()
    {
        var borders = new TableBorders(
            new TopBorder { Val = BorderValues.Single, Size = 8 },
            new LeftBorder { Val = BorderValues.Single, Size = 8 },
            new BottomBorder { Val = BorderValues.Single, Size = 8 },
            new RightBorder { Val = BorderValues.Single, Size = 8 },
            new InsideHorizontalBorder { Val = BorderValues.Single, Size = 8 },
            new InsideVerticalBorder { Val = BorderValues.Single, Size = 8 });

        return new TableProperties(borders);
    }

    private static TableCell CreateCell(string text, int width, bool bold)
    {
        var runProperties = bold ? new RunProperties(new Bold()) : new RunProperties();
        var run = new Run(runProperties, new Text(text ?? ""));
        var paragraph = new Paragraph(run);
        var properties = new TableCellProperties(new TableCellWidth { Width = width.ToString(), Type = TableWidthUnitValues.Dxa });
        return new TableCell(properties, paragraph);
    }

    private static string GetValue<T>(T item, (string Header, string PropertyName, string FiledName) header)
    {
        if (!string.IsNullOrWhiteSpace(header.PropertyName))
        {
            var property = typeof(T).GetProperty(header.PropertyName, BindingFlags.Instance | BindingFlags.Public);
            if (property != null) return property.GetValue(item)?.ToString() ?? "";
        }

        if (!string.IsNullOrWhiteSpace(header.FiledName))
        {
            var field = typeof(T).GetField(header.FiledName, BindingFlags.Instance | BindingFlags.Public);
            if (field != null) return field.GetValue(item)?.ToString() ?? "";
        }

        return "";
    }
}
