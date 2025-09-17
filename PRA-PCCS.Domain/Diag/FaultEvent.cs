using PRA.PCCS.Domain.Shared;

namespace PRA.PCCS.Domain.Diag;

public sealed class FaultEvent : DiagEvent
{
    // Tipo Open Interface exacto (p.ej., "DET_Amp48VAFault", "DET_NetworkChangeDiagEvent", etc.)
    public string TypeId { get; private set; } = string.Empty;

    // Tipo lógico interno (bucket) para agregaciones rápidas
    public FaultKind Kind { get; private set; } = FaultKind.Unknown;

    // Campo muy frecuente en Amp* y muchos otros
    public int? VendorSeverity { get; private set; }

    // Metadatos útiles comunes
    public string? UnitName { get; private set; }
    public string? ZoneName { get; private set; }
    public string? Details { get; private set; }

    // Payload específico del tipo (JSON)
    public StructuredData Data { get; private set; } = StructuredData.Empty;

    private FaultEvent() { } // EF

    public FaultEvent(
        int controllerId,
        int externalEventId,
        int eventGroupId,
        string typeId,                   // <-- nuevo
        FaultKind kind,                  // bucket interno
        StructuredData? data = null,     // <-- nuevo
        int? vendorSeverity = null,
        DateTime? addedAtUtc = null,
        string? eventName = null,
        string? stateCode = null,
        int? eventStateId = null,
        int? equipmentTypesId = null,
        int? size = null,
        DiagSeverity? severity = null,
        string? description = null,
        string? unitName = null,
        string? zoneName = null,
        string? details = null)
        : base(controllerId, externalEventId, eventGroupId, DiagEventGroup.Fault,
               addedAtUtc, eventName, stateCode, eventStateId, equipmentTypesId, size, severity, description)
    {
        SetType(typeId);
        SetKind(kind);
        Data = data ?? StructuredData.Empty;
        VendorSeverity = vendorSeverity;
        UnitName = string.IsNullOrWhiteSpace(unitName) ? null : unitName.Trim();
        ZoneName = string.IsNullOrWhiteSpace(zoneName) ? null : zoneName.Trim();
        Details = string.IsNullOrWhiteSpace(details) ? null : details.Trim();
    }

    public void SetType(string typeId)
    {
        if (string.IsNullOrWhiteSpace(typeId)) throw new ArgumentException("Fault TypeId required.", nameof(typeId));
        TypeId = typeId.Trim();
    }

    public void SetKind(FaultKind kind) => Kind = kind;
    public void SetVendorSeverity(int? value) => VendorSeverity = value;
    public void SetUnit(string? unitName) => UnitName = string.IsNullOrWhiteSpace(unitName) ? null : unitName.Trim();
    public void SetZone(string? zoneName) => ZoneName = string.IsNullOrWhiteSpace(zoneName) ? null : zoneName.Trim();
    public void SetDetails(string? details) => Details = string.IsNullOrWhiteSpace(details) ? null : details.Trim();

    public void SetData(StructuredData data) => Data = data ?? StructuredData.Empty;
}

