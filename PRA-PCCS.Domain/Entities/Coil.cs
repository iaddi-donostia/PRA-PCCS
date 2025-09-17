using PRA.PCCS.Domain.Enums;
using PRA.PCCS.Domain.ValueObjects;

namespace PRA.PCCS.Domain.Entities;

public sealed class Coil
{
    public CoilAddress Address { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public CoilType Type { get; private set; }

    private bool _value;
    public bool Value => _value;

    public Controller? Controller { get; private set; }

    private Coil() { }

    public Coil(CoilAddress address, string name, CoilType type, Controller? controller = null, bool initialValue = false)
    {
        if (address.Value == 0 && type != CoilType.Config)
            throw new ArgumentOutOfRangeException(nameof(address), "Address should be > 0 for non-config coils.");

        Address = address;
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name required") : name.Trim();
        Type = type;
        Controller = controller;
        _value = initialValue;
    }

    public bool Set(bool newValue)
    {
        var changed = _value != newValue;
        _value = newValue;
        return changed;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Name required");
        Name = newName.Trim();
    }
}
