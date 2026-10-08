using OfficeOpenXml;
using PluginsContracts;
using System.ComponentModel;

namespace TextExcel.Plugin;

public sealed class TextExcelReportPlugin : IReportDocumentWithContextTextsContract
{
    public string DocumentFormat => "xlsx";

    public Task CreateDocumentAsync(string filePath, string header, List<string> paragraphs)
    {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));
        if (string.IsNullOrWhiteSpace(header)) throw new ArgumentNullException(nameof(header));
        if (paragraphs == null) throw new ArgumentNullException(nameof(paragraphs));
        if (paragraphs.Count == 0) throw new ArgumentOutOfRangeException(nameof(paragraphs));
        if (paragraphs.Any(string.IsNullOrWhiteSpace)) throw new ArgumentNullException(nameof(paragraphs));

        ExcelPackage.License.SetNonCommercialPersonal("Аделя Валиуллова");

        return Task.Run(() =>
        {
            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Отчет");

            sheet.Cells[1, 1, 1, 4].Merge = true;
            sheet.Cells[1, 1].Value = header;
            sheet.Cells[1, 1].Style.Font.Bold = true;
            sheet.Cells[1, 1].Style.Font.Size = 14;

            for (var i = 0; i < paragraphs.Count; i++)
            {
                sheet.Cells[i + 3, 1, i + 3, 4].Merge = true;
                sheet.Cells[i + 3, 1].Value = paragraphs[i];
                sheet.Cells[i + 3, 1].Style.WrapText = true;
            }

            sheet.Column(1).Width = 25;
            sheet.Column(2).Width = 25;
            sheet.Column(3).Width = 25;
            sheet.Column(4).Width = 25;

            package.SaveAs(new FileInfo(filePath));
        });
    }
}
