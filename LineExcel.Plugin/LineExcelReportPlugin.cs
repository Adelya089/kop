using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using PluginsContracts;

namespace LineExcel.Plugin;

public sealed class LineExcelReportPlugin : IReportDocumentWithChartLineContract
{
    public string DocumentFormat => "xlsx";

    public Task CreateDocumentAsync(
        string filePath,
        string header,
        string chartTitle,
        Dictionary<string, List<(int Parameter, double Value)>> series)
    {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));
        if (string.IsNullOrWhiteSpace(header)) throw new ArgumentNullException(nameof(header));
        if (string.IsNullOrWhiteSpace(chartTitle)) throw new ArgumentNullException(nameof(chartTitle));
        if (series == null) throw new ArgumentNullException(nameof(series));
        if (series.Count == 0) throw new ArgumentOutOfRangeException(nameof(series));
        if (series.Any(x => x.Value == null || x.Value.Count == 0)) throw new ArgumentOutOfRangeException(nameof(series));

        var parameters = series.First().Value.Select(x => x.Parameter).ToList();
        if (series.Any(x => !x.Value.Select(v => v.Parameter).SequenceEqual(parameters)))
            throw new ArgumentException("В разных сериях различаются параметры.", nameof(series));

        ExcelPackage.License.SetNonCommercialPersonal("Аделя Валиуллова");

        return Task.Run(() =>
        {
            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Диаграмма");

            sheet.Cells[1, 1].Value = header;
            sheet.Cells[1, 1].Style.Font.Bold = true;
            sheet.Cells[1, 1].Style.Font.Size = 14;

            sheet.Cells[3, 1].Value = "Параметр";
            for (var i = 0; i < parameters.Count; i++)
                sheet.Cells[i + 4, 1].Value = parameters[i];

            var column = 2;
            foreach (var item in series)
            {
                sheet.Cells[3, column].Value = item.Key;
                for (var i = 0; i < item.Value.Count; i++)
                    sheet.Cells[i + 4, column].Value = item.Value[i].Value;
                column++;
            }

            var chart = sheet.Drawings.AddChart("lineChart", eChartType.LineMarkers);
            chart.Title.Text = chartTitle;
            chart.SetPosition(2, 0, column + 1, 0);
            chart.SetSize(700, 400);

            for (var c = 2; c < column; c++)
            {
                var chartSeries = chart.Series.Add(
                    sheet.Cells[4, c, parameters.Count + 3, c],
                    sheet.Cells[4, 1, parameters.Count + 3, 1]);
                chartSeries.Header = sheet.Cells[3, c].Text;
            }

            sheet.Cells.AutoFitColumns();
            package.SaveAs(new FileInfo(filePath));
        });
    }
}
