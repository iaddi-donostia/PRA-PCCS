namespace PRA.PCCS.Domain.Controllers;

public sealed class ConfiguredUnit
{
    public int Id { get; private set; }                    // antes: ConfiguredUnitId
    public string Name { get; private set; } = string.Empty;        // antes: ConfiguredUnitName
    public string Description { get; private set; } = string.Empty; // antes: ConfiguredUnitDescription
    public string Host { get; private set; } = string.Empty;        // antes: ConfiguredUnitHost

    public bool IsConnected { get; private set; }          // antes: Connected
    public bool IsObsolete { get; private set; }          // antes: Obsolete

    // Relación con Controller
    public int ControllerId { get; private set; }

    // Requerido por EF
    private ConfiguredUnit() { }

    public ConfiguredUnit(string name, string host, int controllerId, string? description = null)
    {
        Rename(name);
        SetHost(host);
        SetDescription(description ?? string.Empty);
        ControllerId = controllerId;
    }

    // ---- Invariantes / setters controlados ----
    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Configured unit name required.", nameof(name));
        Name = name.Trim();
    }

    public void SetDescription(string description) => Description = (description ?? string.Empty).Trim();

    public void SetHost(string host)
    {
        if (string.IsNullOrWhiteSpace(host)) throw new ArgumentException("Host required.", nameof(host));
        Host = host.Trim();
    }

    public void SetConnected(bool value) => IsConnected = value;
    public void SetObsolete(bool value) => IsObsolete = value;
}
