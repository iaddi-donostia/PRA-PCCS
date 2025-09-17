using PRA.PCCS.Domain.Enums;
using PRA.PCCS.Domain.ValueObjects;

namespace PRA.PCCS.Domain.Entities;

public sealed class Zone
{
    public ZoneId Id { get; private set; }
    public string Name { get; private set; }
    public ZoneState State { get; private set; }

    public Controller? Controller { get; private set; }

    private Zone() { }

    internal Zone(ZoneId id, string name, ZoneState state, Controller? controller = null)
    {
        Id = id;
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name required") : name.Trim();
        State = state;
        Controller = controller;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Zone name required");
        Name = newName.Trim();
    }

    public void SetState(ZoneState state) => State = state;
}
