namespace PluginsContracts;

public interface IReportDocumentWithChartLineContract : IReportDocumentContract
{
    Task CreateDocumentAsync(
        string filePath,
        string header,
        string chartTitle,
        Dictionary<string, List<(int Parameter, double Value)>> series);
}
