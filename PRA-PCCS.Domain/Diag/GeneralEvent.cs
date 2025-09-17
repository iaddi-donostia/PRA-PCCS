namespace PRA.PCCS.Domain.Diag;

public sealed class GeneralEvent : DiagEvent
{
    private GeneralEvent() { } // EF

    public GeneralEvent(
        int controllerId,
        int externalEventId,
        int eventGroupId,
        DateTime? addedAtUtc = null,
        string? eventName = null,
        string? stateCode = null,
        int? eventStateId = null,
        int? equipmentTypesId = null,
        int? size = null,
        DiagSeverity? severity = null,
        string? description = null)
        : base(controllerId, externalEventId, eventGroupId, DiagEventGroup.General,
               addedAtUtc, eventName, stateCode, eventStateId, equipmentTypesId, size, severity, description)
    { }
}

