using System.Text.Json;

namespace LibraryHost;

public sealed class AppConfig
{
    public string PluginDirectory { get; set; } = "Plugins";
    public string LicenseFile { get; set; } = "License/license.lic";

    public static AppConfig Load()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        if (!File.Exists(path))
            return new AppConfig();

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
    }
}
