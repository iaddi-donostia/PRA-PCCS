namespace PRA.PCCS.Domain.Controllers;

public sealed class Zone
{
    public int Id { get; private set; }                 // antes: ZoneId
    public string Name { get; private set; } = string.Empty; // antes: ZoneName

    public bool? InUse { get; private set; }
    public int? Priority { get; private set; }         // antes: Priotity (typo)
    public bool? HasFault { get; private set; }
    public bool? HasBgm { get; private set; }           // antes: HasBGM
    public bool IsObsolete { get; private set; }       // antes: Obsolete

    // Relación con Controller
    public int ControllerId { get; private set; }
    // (no exponemos la navegación aquí para mantener el dominio limpio;
    // si la quieres pública de solo lectura, podemos añadirla)

    // EF
    private Zone() { }

    public Zone(string name, int controllerId)
    {
        Rename(name);
        ControllerId = controllerId;
    }

    // --- pequeñas invariantes / setters controlados ---
    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Zone name required.", nameof(name));
        Name = name.Trim();
    }

    public void SetInUse(bool? inUse) => InUse = inUse;
    public void SetPriority(int? priority) => Priority = priority;
    public void SetHasFault(bool? hasFault) => HasFault = hasFault;
    public void SetHasBgm(bool? hasBgm) => HasBgm = hasBgm;
    public void SetObsolete(bool value) => IsObsolete = value;
}
