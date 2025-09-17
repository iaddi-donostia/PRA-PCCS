namespace PRA.PCCS.Domain.ValueObjects;

public readonly record struct ControllerId(Guid Value)
{
    public static ControllerId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
    public static implicit operator Guid(ControllerId id) => id.Value;
    public static ControllerId From(Guid value) => new(value);
}
