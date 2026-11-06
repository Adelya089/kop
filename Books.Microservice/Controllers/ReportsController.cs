
using Books.Microservice.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Globalization;
using System.Text;

namespace Books.Microservice.Controllers;

[ApiController]
[Route("reports")]
public sealed class ReportsController(BookDatabase database) : ControllerBase
{
    [HttpGet("free-books.xlsx")]
    public async Task<IActionResult> FreeBooksExcel()
    {
        var books = (await database.AllAsync())
                .Where(b => b.Price is null or 0m).ToList();

        using var workbook = new XLWorkbook();
        var sheet = workbook.AddWorksheet("Бесплатные книги");
        sheet.Cell(1, 1).Value = "Бесплатные книги";
        sheet.Cell(1, 1).Style.Font.Bold = true;

        for (var i = 0; i < books.Count; i++)
            sheet.Cell(i + 2, 1).Value =
                $"{books[i].Title} — {books[i].Description}";

        sheet.Column(1).Width = 85;

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "free-books.xlsx");
    }

    [HttpGet("free-by-genre.pdf")]
    public async Task<IActionResult> FreeByGenrePdf()
    {
        var groups = (await database.AllAsync())
            .Where(b => b.Price is null or 0m)
            .GroupBy(b => b.Genre)
            .Select(g => (Genre: g.Key, Count: g.Count()))
            .OrderBy(x => x.Genre).ToList();

        var svg = PieSvg(groups);

        var pdf = QuestPDF.Fluent.Document.Create(document =>
            document.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(35);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("Бесплатные книги по жанрам")
                    .FontSize(18).Bold();

                page.Content().Column(column =>
                {
                    column.Spacing(12);

                    if (groups.Count == 0)
                    {
                        column.Item().Text("Бесплатных книг нет.");
                    }
                    else
                    {
                        column.Item().Height(240).Svg(svg);

                        foreach (var group in groups)
                            column.Item().Text(
                                $"{group.Genre}: {group.Count}");
                    }
                });
            })).GeneratePdf();

        return File(pdf, "application/pdf", "free-by-genre.pdf");
    }

    [HttpGet("all-books.docx")]
    public async Task<IActionResult> AllBooksWord()
    {
        var books = await database.AllAsync();

        using var stream = new MemoryStream();

        using (var doc = WordprocessingDocument.Create(
            stream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
        {
            var main = doc.AddMainDocumentPart();

            main.Document =
                new DocumentFormat.OpenXml.Wordprocessing.Document(
                    new Body());

            var body = main.Document.Body!;

            body.Append(new Paragraph(
                new Run(new Text("Список книг"))));

            var table = new Table();

            table.Append(new TableProperties(new TableBorders(
                new TopBorder { Val = BorderValues.Single },
                new BottomBorder { Val = BorderValues.Single },
                new LeftBorder { Val = BorderValues.Single },
                new RightBorder { Val = BorderValues.Single },
                new InsideHorizontalBorder { Val = BorderValues.Single },
                new InsideVerticalBorder { Val = BorderValues.Single }
            )));

            table.Append(Row(
                "ID", "Название", "Жанр", "Стоимость"));

            foreach (var book in books)
            {
                table.Append(Row(
                    book.Id.ToString(),
                    book.Title,
                    book.Genre,
                    book.Price.HasValue
                        ? book.Price.Value.ToString(
                            "0.00",
                            CultureInfo.GetCultureInfo("ru-RU"))
                        : "бесплатная"));
            }

            body.Append(table);
            main.Document.Save();
        }

        return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "all-books.docx");
    }

    [HttpGet("book-prices.xlsx")]
    public async Task<IActionResult> BookPricesExcel()
    {
        var books = (await database.AllAsync())
            .OrderBy(b => b.Id).ToList();

        if (books.Count == 0)
            return NotFound("Нет книг для диаграммы.");

        ExcelPackage.License.SetNonCommercialPersonal("Student");

        using var package = new ExcelPackage();

        var sheet = package.Workbook.Worksheets.Add("Диаграмма");

        sheet.Cells[1, 1].Value = "Стоимость книг";
        sheet.Cells[1, 1].Style.Font.Bold = true;
        sheet.Cells[1, 1].Style.Font.Size = 14;

        sheet.Cells[3, 1].Value = "ID книги";
        sheet.Cells[3, 2].Value = "Стоимость";

        for (var i = 0; i < books.Count; i++)
        {
            sheet.Cells[i + 4, 1].Value = books[i].Id;
            sheet.Cells[i + 4, 2].Value =
                (double)(books[i].Price ?? 0m);
        }

        var chart = sheet.Drawings.AddChart(
            "lineChart", eChartType.LineMarkers);

        chart.Title.Text = "Стоимость книг по идентификаторам";
        chart.SetPosition(2, 0, 4, 0);
        chart.SetSize(700, 400);

        var series = chart.Series.Add(
            sheet.Cells[4, 2, books.Count + 3, 2],
            sheet.Cells[4, 1, books.Count + 3, 1]);

        series.Header = "Стоимость";

        sheet.Cells.AutoFitColumns();

        return File(package.GetAsByteArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "book-prices.xlsx");
    }

    private static TableRow Row(params string[] values)
    {
        var row = new TableRow();

        foreach (var value in values)
        {
            row.Append(new TableCell(
                new Paragraph(new Run(new Text(value)))));
        }

        return row;
    }

    private static string PieSvg(
        List<(string Genre, int Count)> groups)
    {
        var colors = new[]
        {
            "#4472C4", "#ED7D31", "#70AD47",
            "#A5A5A5", "#FFC000", "#8064A2"
        };

        var total = groups.Sum(g => g.Count);

        var xml = new StringBuilder(
            "<svg xmlns='http://www.w3.org/2000/svg' " +
            "width='510' height='260' viewBox='0 0 510 260'>");

        double start = -Math.PI / 2;

        for (var i = 0; i < groups.Count; i++)
        {
            var fraction = (double)groups[i].Count / total;

            if (groups.Count == 1)
            {
                xml.Append(
                    $"<circle cx='130' cy='130' r='95' " +
                    $"fill='{colors[i % colors.Length]}'/>");
            }
            else
            {
                var end = start + fraction * 2 * Math.PI;

                var x1 = 130 + 95 * Math.Cos(start);
                var y1 = 130 + 95 * Math.Sin(start);
                var x2 = 130 + 95 * Math.Cos(end);
                var y2 = 130 + 95 * Math.Sin(end);

                string F(double n) =>
                    n.ToString("0.###", CultureInfo.InvariantCulture);

                xml.Append(
                    $"<path d='M 130 130 L {F(x1)} {F(y1)} " +
                    $"A 95 95 0 {(fraction > 0.5 ? 1 : 0)} 1 " +
                    $"{F(x2)} {F(y2)} Z' " +
                    $"fill='{colors[i % colors.Length]}'/>");

                start = end;
            }

            var y = 30 + i * 28;
            var label =
                System.Security.SecurityElement.Escape(groups[i].Genre);

            xml.Append(
                $"<rect x='270' y='{y}' width='13' height='13' " +
                $"fill='{colors[i % colors.Length]}'/>");

            xml.Append(
                $"<text x='290' y='{y + 12}' " +
                $"font-family='DejaVu Sans' font-size='13'>" +
                $"{label}: {groups[i].Count}</text>");
        }

        xml.Append("</svg>");

        return xml.ToString();
    }
}
