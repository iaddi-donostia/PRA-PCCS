namespace PRA.PCCS.Domain.Controllers;

public sealed class ZoneGroup
{
    public int Id { get; private set; }                    
    public string Name { get; private set; } = string.Empty; 

    public bool? InUse { get; private set; }
    public int? Priority { get; private set; }             
    public bool? HasFault { get; private set; }
    public bool? HasBgm { get; private set; }              
    public bool IsObsolete { get; private set; }           

    // Relación con Controller
    public int ControllerId { get; private set; }

    // Relación con Zones (composición)
    private readonly List<Zone> _zones = new();
    public IReadOnlyList<Zone> Zones => _zones;

    // EF
    private ZoneGroup() { }

    public ZoneGroup(string name, int controllerId)
    {
        Rename(name);
        ControllerId = controllerId;
    }

    // --- invariantes / setters controlados ---
    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Group name required.", nameof(name));

        Name = name.Trim();
    }

    public void SetInUse(bool? inUse) => InUse = inUse;
    public void SetPriority(int? priority) => Priority = priority;
    public void SetHasFault(bool? hasFault) => HasFault = hasFault;
    public void SetHasBgm(bool? hasBgm) => HasBgm = hasBgm;
    public void SetObsolete(bool value) => IsObsolete = value;

    // --- gestión de Zones ---
    public Zone AddZone(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Zone name required.", nameof(name));

        var zone = new Zone(name.Trim(), ControllerId);
        _zones.Add(zone);
        return zone;
    }

    public bool RemoveZone(int zoneId)
    {
        var z = _zones.FirstOrDefault(x => x.Id == zoneId);
        if (z is null) return false;
        _zones.Remove(z);
        return true;
    }
}
