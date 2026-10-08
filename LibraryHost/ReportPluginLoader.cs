using System.Reflection;

namespace LibraryHost;

public static class ReportPluginLoader
{
    public static List<T> Load<T>(string pluginDirectory) where T : class
    {
        var fullPath = Path.Combine(AppContext.BaseDirectory, pluginDirectory);
        Directory.CreateDirectory(fullPath);
        var result = new List<T>();

        foreach (var file in Directory.GetFiles(fullPath, "*.Plugin.dll"))
        {
            var assembly = Assembly.LoadFrom(file);
            Type[] types;

            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(x => x != null).Cast<Type>().ToArray();
            }

            foreach (var type in types.Where(x =>
                         x is { IsClass: true, IsAbstract: false } &&
                         typeof(T).IsAssignableFrom(x)))
            {
                if (Activator.CreateInstance(type) is T plugin)
                    result.Add(plugin);
            }
        }

        return result;
    }
}
