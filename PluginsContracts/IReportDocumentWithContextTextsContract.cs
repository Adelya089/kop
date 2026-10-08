namespace PluginsContracts;

public interface IReportDocumentWithContextTextsContract : IReportDocumentContract
{
    Task CreateDocumentAsync(string filePath, string header, List<string> paragraphs);
}
