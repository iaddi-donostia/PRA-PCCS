namespace PRA.PCCS.Domain.Diag;

public static class GeneralEventFactory
{
    // ---- helper base ----
    private static GeneralEvent New(
        int controllerId, int externalEventId, int eventGroupId,
        DateTime? atUtc,
        DiagSeverity? severity,
        string? eventName,
        string? stateCode,
        int? eventStateId,
        int? equipmentTypesId,
        int? size,
        string? description)
    {
        return new GeneralEvent(
            controllerId: controllerId,
            externalEventId: externalEventId,
            eventGroupId: eventGroupId,
            addedAtUtc: atUtc,
            eventName: eventName,
            stateCode: stateCode,
            eventStateId: eventStateId,
            equipmentTypesId: equipmentTypesId,
            size: size,
            severity: severity,
            description: description
        );
    }

    // -------------------------
    // Creación "genérica"
    // -------------------------
    public static GeneralEvent Create(
        int controllerId, int externalEventId, int eventGroupId,
        DateTime? atUtc = null,
        DiagSeverity? severity = null,
        string? eventName = null,
        string? stateCode = null,
        int? eventStateId = null,
        int? equipmentTypesId = null,
        int? size = null,
        string? description = null)
        => New(controllerId, externalEventId, eventGroupId, atUtc, severity, eventName, stateCode, eventStateId, equipmentTypesId, size, description);

    // Atajos de severidad
    public static GeneralEvent Info(
        int controllerId, int externalEventId, int eventGroupId,
        string? description = null,
        DateTime? atUtc = null,
        string? eventName = null,
        string? stateCode = null,
        int? eventStateId = null,
        int? equipmentTypesId = null,
        int? size = null)
        => New(controllerId, externalEventId, eventGroupId, atUtc, DiagSeverity.Info, eventName, stateCode, eventStateId, equipmentTypesId, size, description);

    public static GeneralEvent Warning(
        int controllerId, int externalEventId, int eventGroupId,
        string? description = null,
        DateTime? atUtc = null,
        string? eventName = null,
        string? stateCode = null,
        int? eventStateId = null,
        int? equipmentTypesId = null,
        int? size = null)
        => New(controllerId, externalEventId, eventGroupId, atUtc, DiagSeverity.Warning, eventName, stateCode, eventStateId, equipmentTypesId, size, description);

    public static GeneralEvent Error(
        int controllerId, int externalEventId, int eventGroupId,
        string? description = null,
        DateTime? atUtc = null,
        string? eventName = null,
        string? stateCode = null,
        int? eventStateId = null,
        int? equipmentTypesId = null,
        int? size = null)
        => New(controllerId, externalEventId, eventGroupId, atUtc, DiagSeverity.Error, eventName, stateCode, eventStateId, equipmentTypesId, size, description);

    public static GeneralEvent Critical(
        int controllerId, int externalEventId, int eventGroupId,
        string? description = null,
        DateTime? atUtc = null,
        string? eventName = null,
        string? stateCode = null,
        int? eventStateId = null,
        int? equipmentTypesId = null,
        int? size = null)
        => New(controllerId, externalEventId, eventGroupId, atUtc, DiagSeverity.Critical, eventName, stateCode, eventStateId, equipmentTypesId, size, description);

    // -------------------------
    // Eventos de ciclo de vida
    // (incluyen originator opcional)
    // -------------------------
    public static GeneralEvent Added(
        int controllerId, int externalEventId, int eventGroupId,
        int? addOriginatorId = null,
        DateTime? atUtc = null,
        string? description = null,
        string? eventName = null,
        string? stateCode = null,
        int? eventStateId = null,
        int? equipmentTypesId = null,
        int? size = null,
        DiagSeverity? severity = null)
    {
        var ev = New(controllerId, externalEventId, eventGroupId, atUtc, severity, eventName, stateCode, eventStateId, equipmentTypesId, size, description);
        ev.SetAdded(atUtc ?? DateTime.UtcNow, addOriginatorId);
        return ev;
    }

    public static GeneralEvent Reset(
        int controllerId, int externalEventId, int eventGroupId,
        int? resetOriginatorId = null,
        DateTime? atUtc = null,
        string? description = null,
        DiagSeverity? severity = null)
    {
        var ev = New(controllerId, externalEventId, eventGroupId, null, severity, null, null, null, null, null, description);
        ev.SetReset(atUtc ?? DateTime.UtcNow, resetOriginatorId);
        return ev;
    }

    public static GeneralEvent Resolved(
        int controllerId, int externalEventId, int eventGroupId,
        int? resolveOriginatorId = null,
        DateTime? atUtc = null,
        string? description = null,
        DiagSeverity? severity = null)
    {
        var ev = New(controllerId, externalEventId, eventGroupId, null, severity, null, null, null, null, null, description);
        ev.SetResolved(atUtc ?? DateTime.UtcNow, resolveOriginatorId);
        return ev;
    }

    public static GeneralEvent Acknowledged(
        int controllerId, int externalEventId, int eventGroupId,
        int? acknowledgedOriginatorId = null,
        DateTime? atUtc = null,
        string? description = null,
        DiagSeverity? severity = null)
    {
        var ev = New(controllerId, externalEventId, eventGroupId, null, severity, null, null, null, null, null, description);
        ev.SetAcknowledged(atUtc ?? DateTime.UtcNow, acknowledgedOriginatorId);
        return ev;
    }

    // -------------------------
    // Atajo con estado/catálogo
    // -------------------------
    public static GeneralEvent WithState(
        int controllerId, int externalEventId, int eventGroupId,
        string stateCode,
        int? eventStateId = null,
        DateTime? atUtc = null,
        DiagSeverity? severity = null,
        string? description = null,
        string? eventName = null,
        int? equipmentTypesId = null,
        int? size = null)
    {
        var ev = New(controllerId, externalEventId, eventGroupId, atUtc, severity, eventName, stateCode, eventStateId, equipmentTypesId, size, description);
        ev.SetState(stateCode, eventStateId);
        return ev;
    }
}
