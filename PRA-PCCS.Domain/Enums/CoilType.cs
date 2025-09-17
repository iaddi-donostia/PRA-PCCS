namespace PRA.PCCS.Domain.Enums;

public enum CoilType
{
    Unknown = 0,
    Command = 1,   // acciones (LaunchCall, StopCall, etc.)
    Status = 2,    // estados (IsBusy, IsFaulted, etc.)
    Config = 3     // flags de configuración
}
