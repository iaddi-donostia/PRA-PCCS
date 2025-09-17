namespace PRA.PCCS.Domain.Diag;

public sealed class CallEvent : DiagEvent
{
    public int? CallId { get; private set; }        // Id de llamada del protocolo
    public CallEventKind Kind { get; private set; } // Start/End/Timeout/...

    public string? Routing { get; private set; }    // Zonas/Grupos (puede ser texto o JSON)
    public string? Content { get; private set; }    // Speech/AudioInput/Message/Chime...

    private CallEvent() { } // EF

    public CallEvent(
        int controllerId,
        int externalEventId,
        int eventGroupId,
        CallEventKind kind,
        int? callId = null,
        DateTime? addedAtUtc = null,
        string? eventName = null,
        string? stateCode = null,
        int? eventStateId = null,
        int? equipmentTypesId = null,
        int? size = null,
        DiagSeverity? severity = null,
        string? description = null,
        string? routing = null,
        string? content = null)
        : base(controllerId, externalEventId, eventGroupId, DiagEventGroup.Call,
               addedAtUtc, eventName, stateCode, eventStateId, equipmentTypesId, size, severity, description)
    {
        Kind = kind;
        CallId = callId;
        Routing = string.IsNullOrWhiteSpace(routing) ? null : routing.Trim();
        Content = string.IsNullOrWhiteSpace(content) ? null : content.Trim();
    }

    public void SetKind(CallEventKind kind) => Kind = kind;
    public void SetCallId(int? id) => CallId = id;
    public void SetRouting(string? routing) => Routing = string.IsNullOrWhiteSpace(routing) ? null : routing.Trim();
    public void SetContent(string? content) => Content = string.IsNullOrWhiteSpace(content) ? null : content.Trim();
}

