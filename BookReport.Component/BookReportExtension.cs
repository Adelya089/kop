using ComponentContracts;
using System.ComponentModel.Composition;
namespace BookReport.Component;
[Export(typeof(IComponentContract))]
public sealed class BookReportExtension : IComponentContract
{
    public string Id=>"book-report"; public string Title=>"Книги по жанру"; public ComponentKind Kind=>ComponentKind.Report; public LicenseLevel RequiredLicense=>LicenseLevel.Advanced; public UserControl CreateControl()=>new BookReportControl();
}
