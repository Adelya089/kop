using ComponentContracts;

namespace LibraryHost;

public static class LicenseService
{
    public static LicenseLevel ReadLicense(AppConfig config)
    {
        var path = Path.Combine(AppContext.BaseDirectory, config.LicenseFile);

        if (!File.Exists(path))
            return LicenseLevel.Minimal;

        var value = File.ReadAllText(path).Trim().ToUpperInvariant();

        return value switch
        {
            "ADVANCED" => LicenseLevel.Advanced,
            "BASIC" => LicenseLevel.Basic,
            _ => LicenseLevel.Minimal
        };
    }
}
