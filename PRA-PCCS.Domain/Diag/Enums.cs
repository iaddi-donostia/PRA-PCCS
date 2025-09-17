namespace PRA.PCCS.Domain.Diag;

public enum DiagEventGroup { General = 0, Call = 1, Fault = 2 }

// Si más adelante quieres mapear a tus tablas DE_EventState / DE_Originator / etc.,
// podemos añadir enums o tablas de referencia. De momento mantenemos los IDs y códigos sueltos.
public enum DiagSeverity { Info = 0, Warning = 1, Error = 2, Critical = 3 }

public enum FaultSeverity { Low = 0, High = 1 }

public enum CallEventKind
{
    Unknown = 0,
    Start = 1,
    End = 2,
    Timeout = 3,
    ChangeResource = 4,
    Reset = 5,
    Restart = 6
}
public enum FaultKind
{
    Unknown = 0,
    AmpFault = 1,
    PowerSupply = 2,
    Network = 3,
    RemoteOutput = 4,
    BatteryOrCharger = 5,
    Configuration = 6,
    VoIP = 7,
    ZoneLine = 9,
    Synchronization = 10,
    Other = 999
}




