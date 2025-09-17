namespace PRA.PCCS.Domain.ValueObjects;

public readonly record struct CoilAddress(ushort Value)
{
    // Public parameterless constructor (required for some serializers); initializes to 0
    public CoilAddress() : this(0) { }

    public static CoilAddress From(ushort value) => new(value);
    public static implicit operator ushort(CoilAddress a) => a.Value;
    public override string ToString() => Value.ToString();
}
