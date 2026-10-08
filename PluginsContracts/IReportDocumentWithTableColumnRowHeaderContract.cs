namespace PluginsContracts;

public interface IReportDocumentWithTableColumnRowHeaderContract : IReportDocumentContract
{
    Task CreateDocumentAsync<T>(
        string filePath,
        string header,
        List<int> columnsWidth,
        List<int> rowsHeights,
        bool isHeaderFirstRow,
        List<(string Header, string PropertyName, string FiledName)> headers,
        List<T> data);
}
