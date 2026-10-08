using ComponentContracts;
using System.ComponentModel.Composition;
namespace Books.Component;
[Export(typeof(IComponentContract))]
public sealed class BooksExtension : IComponentContract
{
    public string Id => "books"; public string Title => "Книги"; public ComponentKind Kind => ComponentKind.Directory; public LicenseLevel RequiredLicense => LicenseLevel.Basic; public UserControl CreateControl()=>new BooksControl();
}
