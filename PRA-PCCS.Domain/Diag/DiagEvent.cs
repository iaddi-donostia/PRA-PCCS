namespace PRA.PCCS.Domain.Diag;

public abstract class DiagEvent
{
    public int Id { get; private set; }

    // Relación con Controller
    public int ControllerId { get; private set; }

    // === Identidad y clasificación del evento ===
    public int ExternalEventId { get; private set; }          // antes: EventId
    public string? EventName { get; private set; }          // antes: EventName
    public string? StateCode { get; private set; }          // antes: EventState (string)
    public int? EventStateId { get; private set; }          // antes: DE_EventStateId (lookup opcional)
    public int EventGroupId { get; private set; }          // antes: DE_EventGroupId (guardamos el int)
    public DiagEventGroup Group { get; private set; }          // proyección limpia del grupo

    // Equipos / Tipos (si usabas tablas de referencia)
    public int? EquipmentTypesId { get; private set; }          // antes: DE_EquipmentTypesId

    // Tamaño/payload si lo había
    public int? Size { get; private set; }          // antes: size

    // === Timestamps y originators ===
    public DateTime? AddedAtUtc { get; private set; }  // antes: AddTimeStamp
    public int? AddOriginatorId { get; private set; }  // antes: AddOriginatorId

    public DateTime? ResetAtUtc { get; private set; }  // antes: ResetTimeStamp
    public int? ResetOriginatorId { get; private set; }  // antes: ResetOriginatorId

    public DateTime? ResolvedAtUtc { get; private set; }  // antes: ResolveTimeStamp
    public int? ResolveOriginatorId { get; private set; }  // antes: ResolveOriginatorId

    public DateTime? AcknowledgedAtUtc { get; private set; }  // antes: AcknowledgedTimeStamp
    public int? AcknowledgedOriginatorId { get; private set; } // antes: AcknowledgedOriginatorId

    // Metadata adicional (opcional)
    public DiagSeverity? Severity { get; private set; }
    public string? Description { get; private set; }

    protected DiagEvent() { } // EF

    protected DiagEvent(
        int controllerId,
        int externalEventId,
        int eventGroupId,
        DiagEventGroup group,
        DateTime? addedAtUtc = null,
        string? eventName = null,
        string? stateCode = null,
        int? eventStateId = null,
        int? equipmentTypesId = null,
        int? size = null,
        DiagSeverity? severity = null,
        string? description = null)
    {
        ControllerId = controllerId;
        ExternalEventId = externalEventId;
        EventGroupId = eventGroupId;
        Group = group;

        AddedAtUtc = addedAtUtc;
        EventName = string.IsNullOrWhiteSpace(eventName) ? null : eventName.Trim();
        StateCode = string.IsNullOrWhiteSpace(stateCode) ? null : stateCode.Trim();
        EventStateId = eventStateId;
        EquipmentTypesId = equipmentTypesId;
        Size = size;
        Severity = severity;
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
    }

    // --- setters controlados (equivalentes a tus campos sueltos) ---
    public void SetState(string? code, int? stateId = null)
    {
        StateCode = string.IsNullOrWhiteSpace(code) ? null : code.Trim();
        EventStateId = stateId;
    }

    public void SetEquipmentTypesId(int? id) => EquipmentTypesId = id;
    public void SetSize(int? size) => Size = size;

    public void SetAdded(DateTime utc, int? originatorId = null)
    {
        AddedAtUtc = utc;
        AddOriginatorId = originatorId;
    }

    public void SetReset(DateTime utc, int? originatorId = null)
    {
        ResetAtUtc = utc;
        ResetOriginatorId = originatorId;
    }

    public void SetResolved(DateTime utc, int? originatorId = null)
    {
        ResolvedAtUtc = utc;
        ResolveOriginatorId = originatorId;
    }

    public void SetAcknowledged(DateTime utc, int? originatorId = null)
    {
        AcknowledgedAtUtc = utc;
        AcknowledgedOriginatorId = originatorId;
    }

    public void SetSeverity(DiagSeverity? severity) => Severity = severity;
    public void SetDescription(string? description) =>
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
}

