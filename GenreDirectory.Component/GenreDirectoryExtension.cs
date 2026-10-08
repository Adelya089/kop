using ComponentContracts;
using System.ComponentModel.Composition;
namespace GenreDirectory.Component;
[Export(typeof(IComponentContract))]
public sealed class GenreDirectoryExtension : IComponentContract
{
    public string Id => "genres";
    public string Title => "Жанры книг";
    public ComponentKind Kind => ComponentKind.Directory;
    public LicenseLevel RequiredLicense => LicenseLevel.Minimal;
    public UserControl CreateControl() => new GenreDirectoryControl();
}
