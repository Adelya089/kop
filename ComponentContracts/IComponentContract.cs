using System.Windows.Forms;

namespace ComponentContracts
{
    public interface IComponentContract
    {
        string Id { get; }

        string Title { get; }

        ComponentKind Kind { get; }

        LicenseLevel RequiredLicense { get; }

        UserControl CreateControl();
    }
}