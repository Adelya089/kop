using ComponentContracts;

namespace LibraryHost;

public partial class MainForm : Form
{
    private readonly Dictionary<string, IComponentContract> _components = new();

    private readonly AppConfig _config;
    private readonly LicenseLevel _license;

    public MainForm()
    {
        InitializeComponent();

        _config = AppConfig.Load();
        _license = LicenseService.ReadLicense(_config);

        try
        {
            var extensions = PluginLoader.Load(_config.PluginDirectory);

            foreach (var extension in extensions.Where(x => x.RequiredLicense <= _license))
            {
                _components[extension.Id] = extension;

                var menuItem = new ToolStripMenuItem
                {
                    Text = extension.Title
                };

                menuItem.Click += (_, _) => OpenControl(extension.Id);

                if (extension.Kind == ComponentKind.Directory)
                {
                    directoriesToolStripMenuItem.DropDownItems.Add(menuItem);
                }
                else
                {
                    reportsToolStripMenuItem.DropDownItems.Add(menuItem);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Ошибка при загрузке компонентов",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void OpenControl(string id)
    {
        if (!_components.TryGetValue(id, out var extension))
            return;

        foreach (TabPage page in tabControl1.TabPages)
        {
            if (page.Tag?.ToString() == id)
            {
                tabControl1.SelectedTab = page;
                return;
            }
        }

        var control = extension.CreateControl();
        control.Dock = DockStyle.Fill;

        var tabPage = new TabPage
        {
            Text = extension.Title,
            Tag = id
        };

        tabPage.Controls.Add(control);
        tabControl1.TabPages.Add(tabPage);
        tabControl1.SelectedTab = tabPage;
    }

    private void TabControl1_DoubleClick(object? sender, EventArgs e)
    {
        if (tabControl1.SelectedTab is null)
            return;

        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
    }
}
