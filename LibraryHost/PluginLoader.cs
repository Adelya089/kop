using ComponentContracts;
using System.ComponentModel.Composition.Hosting;

namespace LibraryHost;

public static class PluginLoader
{
    public static List<IComponentContract> Load(string pluginDirectory)
    {
        var fullPath = Path.Combine(AppContext.BaseDirectory, pluginDirectory);

        Directory.CreateDirectory(fullPath);

        var catalog = new DirectoryCatalog(fullPath, "*.Component.dll");
        var container = new CompositionContainer(catalog);

        return container.GetExportedValues<IComponentContract>().ToList();
    }
}
