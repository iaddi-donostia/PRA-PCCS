using PRA.PCCS.Domain.Enums;
using PRA.PCCS.Domain.ValueObjects;

namespace PRA.PCCS.Domain.Entities;

public sealed class Controller
{
    public ControllerId Id { get; private set; }
    public string Name { get; private set; }
    public string Host { get; private set; }
    public int Port { get; private set; }
    public ControllerState State { get; private set; }

    private readonly List<Zone> _zones = new();
    public IReadOnlyCollection<Zone> Zones => _zones;

    private Controller() { }

    public Controller(ControllerId id, string name, string host, int port)
    {
        Id = id;
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name required") : name.Trim();
        Host = string.IsNullOrWhiteSpace(host) ? throw new ArgumentException("Host required") : host.Trim();
        Port = (port is > 0 and < 65536) ? port : throw new ArgumentOutOfRangeException(nameof(port));
        State = ControllerState.Unknown;
    }

    public void SetState(ControllerState state) => State = state;

    public Zone AddZone(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Zone name required");
        var zone = new Zone(ZoneId.New(), name.Trim(), ZoneState.Idle, this);
        _zones.Add(zone);
        return zone;
    }
}
