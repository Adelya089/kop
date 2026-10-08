namespace PluginsContracts;

public interface IReportDocumentWithChartPieContract : IReportDocumentContract
{
    Task CreateDocumentAsync(
        string filePath,
        string header,
        string chartTitle,
        string seriesName,
        List<(string Parameter, double Value)> series);
}
