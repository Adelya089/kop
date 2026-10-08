using System.Globalization;
using System.Text;
using PluginsContracts;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PiePdf.Plugin;

public sealed class PiePdfReportPlugin : IReportDocumentWithChartPieContract
{
    public string DocumentFormat => "pdf";

    public Task CreateDocumentAsync(
        string filePath,
        string header,
        string chartTitle,
        string seriesName,
        List<(string Parameter, double Value)> series)
    {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));
        if (string.IsNullOrWhiteSpace(header)) throw new ArgumentNullException(nameof(header));
        if (string.IsNullOrWhiteSpace(chartTitle)) throw new ArgumentNullException(nameof(chartTitle));
        if (string.IsNullOrWhiteSpace(seriesName)) throw new ArgumentNullException(nameof(seriesName));
        if (series == null) throw new ArgumentNullException(nameof(series));
        if (series.Count == 0) throw new ArgumentOutOfRangeException(nameof(series));

        var cleanSeries = series.Where(x => x.Value > 0).ToList();
        if (cleanSeries.Count == 0) throw new ArgumentOutOfRangeException(nameof(series));

        QuestPDF.Settings.License = LicenseType.Community;
        var svg = BuildPieSvg(cleanSeries);

        return Task.Run(() =>
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(35);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text(header).FontSize(18).Bold();

                    page.Content().Column(column =>
                    {
                        column.Spacing(12);
                        column.Item().Text(chartTitle).FontSize(15).Bold();
                        column.Item().Text(seriesName).Italic();
                        column.Item().Height(330).Svg(svg);

                        foreach (var item in cleanSeries)
                            column.Item().Text($"{item.Parameter}: {item.Value:0.##}");
                    });
                });
            }).GeneratePdf(filePath);
        });
    }

    private static string BuildPieSvg(List<(string Parameter, double Value)> series)
    {
        var colors = new[] { "#4472C4", "#ED7D31", "#A5A5A5", "#FFC000", "#5B9BD5", "#70AD47" };
        var total = series.Sum(x => x.Value);
        const double cx = 160;
        const double cy = 160;
        const double radius = 120;
        var sb = new StringBuilder();
        sb.Append("<svg xmlns='http://www.w3.org/2000/svg' width='600' height='330' viewBox='0 0 600 330'>");

        if (series.Count == 1)
        {
            sb.Append($"<circle cx='{cx}' cy='{cy}' r='{radius}' fill='{colors[0]}'/>");
        }
        else
        {
            var startAngle = -90.0;
            for (var i = 0; i < series.Count; i++)
            {
                var angle = series[i].Value / total * 360.0;
                var endAngle = startAngle + angle;
                var start = PointOnCircle(cx, cy, radius, startAngle);
                var end = PointOnCircle(cx, cy, radius, endAngle);
                var largeArc = angle > 180 ? 1 : 0;
                var color = colors[i % colors.Length];

                sb.Append($"<path d='M {F(cx)} {F(cy)} L {F(start.X)} {F(start.Y)} A {radius} {radius} 0 {largeArc} 1 {F(end.X)} {F(end.Y)} Z' fill='{color}'/>");
                startAngle = endAngle;
            }
        }

        for (var i = 0; i < series.Count; i++)
        {
            var y = 35 + i * 28;
            var color = colors[i % colors.Length];
            var label = Escape(series[i].Parameter);
            sb.Append($"<rect x='330' y='{y - 12}' width='16' height='16' fill='{color}'/>");
            sb.Append($"<text x='355' y='{y}' font-size='15' font-family='Arial'>{label}: {F(series[i].Value)}</text>");
        }

        sb.Append("</svg>");
        return sb.ToString();
    }

    private static (double X, double Y) PointOnCircle(double cx, double cy, double radius, double angle)
    {
        var radians = angle * Math.PI / 180.0;
        return (cx + radius * Math.Cos(radians), cy + radius * Math.Sin(radians));
    }

    private static string F(double value) => value.ToString("0.###", CultureInfo.InvariantCulture);

    private static string Escape(string value) => value
        .Replace("&", "&amp;")
        .Replace("<", "&lt;")
        .Replace(">", "&gt;")
        .Replace("\"", "&quot;")
        .Replace("'", "&apos;");
}
