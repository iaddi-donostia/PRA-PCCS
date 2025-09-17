namespace PRA.PCCS.Domain.ValueObjects;

public readonly record struct ZoneId(Guid Value)
{
    public static ZoneId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
    public static implicit operator Guid(ZoneId id) => id.Value;
    public static ZoneId From(Guid value) => new(value);
}
