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
                var menuItem = new ToolStripMenuItem { Text = extension.Title };
                menuItem.Click += (_, _) => OpenControl(extension.Id);

                if (extension.Kind == ComponentKind.Directory)
                    directoriesToolStripMenuItem.DropDownItems.Add(menuItem);
                else
                    reportsToolStripMenuItem.DropDownItems.Add(menuItem);
            }

            if (reportsToolStripMenuItem.DropDownItems.Count > 0)
                reportsToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

            var extensionsMenuItem = new ToolStripMenuItem { Text = "Расширения" };
            extensionsMenuItem.Click += (_, _) =>
            {
                using var form = new ExtensionReportsForm(_config.ReportPluginDirectory);
                form.ShowDialog(this);
            };
            reportsToolStripMenuItem.DropDownItems.Add(extensionsMenuItem);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка при загрузке компонентов", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OpenControl(string id)
    {
        foreach (TabPage page in tabControls.TabPages)
        {
            if ((string?)page.Tag == id)
            {
                tabControls.SelectedTab = page;
                return;
            }
        }

        var extension = _components[id];
        var control = extension.CreateControl();
        control.Dock = DockStyle.Fill;

        var pageNew = new TabPage
        {
            Text = extension.Title,
            Tag = id
        };

        pageNew.Controls.Add(control);
        tabControls.TabPages.Add(pageNew);
        tabControls.SelectedTab = pageNew;
    }

    private void TabControls_DoubleClick(object? sender, EventArgs e)
    {
        if (tabControls.SelectedTab != null)
            tabControls.TabPages.Remove(tabControls.SelectedTab);
    }
}
