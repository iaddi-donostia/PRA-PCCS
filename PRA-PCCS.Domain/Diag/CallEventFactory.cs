using System.Text.Json;

namespace PRA.PCCS.Domain.Diag;

public static class CallEventFactory
{
    // Helper interno
    private static CallEvent New(
        int controllerId, int externalEventId, int eventGroupId,
        CallEventKind kind,
        int? callId = null,
        string? routingJson = null,
        string? content = null,
        DateTime? atUtc = null,
        DiagSeverity? severity = null,
        string? description = null
    )
    => new(controllerId, externalEventId, eventGroupId, kind, callId,
           addedAtUtc: atUtc, eventName: null, stateCode: null, eventStateId: null,
           equipmentTypesId: null, size: null, severity: severity, description: description,
           routing: routingJson, content: content);

    private static string? BuildRoutingJson(IEnumerable<string>? zones, IEnumerable<string>? groups)
    {
        var z = zones?.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToArray() ?? Array.Empty<string>();
        var g = groups?.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToArray() ?? Array.Empty<string>();
        if (z.Length == 0 && g.Length == 0) return null;
        return JsonSerializer.Serialize(new { zones = z, groups = g });
    }

    // --- Eventos típicos ---

    public static CallEvent Start(
        int controllerId, int externalEventId, int eventGroupId,
        int callId,
        IEnumerable<string>? zones = null,
        IEnumerable<string>? groups = null,
        string? content = null,
        DateTime? atUtc = null,
        string? description = null
    )
    => New(controllerId, externalEventId, eventGroupId, CallEventKind.Start, callId,
           routingJson: BuildRoutingJson(zones, groups), content: content, atUtc: atUtc,
           severity: DiagSeverity.Info, description: description);

    public static CallEvent End(
        int controllerId, int externalEventId, int eventGroupId,
        int callId,
        DateTime? atUtc = null,
        string? description = null
    )
    => New(controllerId, externalEventId, eventGroupId, CallEventKind.End, callId,
           routingJson: null, content: null, atUtc: atUtc, severity: DiagSeverity.Info, description: description);

    public static CallEvent Timeout(
        int controllerId, int externalEventId, int eventGroupId,
        int callId,
        DateTime? atUtc = null,
        string? description = null
    )
    => New(controllerId, externalEventId, eventGroupId, CallEventKind.Timeout, callId,
           routingJson: null, content: null, atUtc: atUtc, severity: DiagSeverity.Warning, description: description);

    public static CallEvent ChangeResource(
        int controllerId, int externalEventId, int eventGroupId,
        int callId,
        IEnumerable<string>? zones = null,
        IEnumerable<string>? groups = null,
        string? content = null,
        DateTime? atUtc = null,
        string? description = null
    )
    => New(controllerId, externalEventId, eventGroupId, CallEventKind.ChangeResource, callId,
           routingJson: BuildRoutingJson(zones, groups), content: content, atUtc: atUtc,
           severity: DiagSeverity.Info, description: description);

    public static CallEvent Reset(
        int controllerId, int externalEventId, int eventGroupId,
        int? callId = null,
        DateTime? atUtc = null,
        string? description = null
    )
    => New(controllerId, externalEventId, eventGroupId, CallEventKind.Reset, callId,
           routingJson: null, content: null, atUtc: atUtc, severity: DiagSeverity.Info, description: description);

    public static CallEvent Restart(
        int controllerId, int externalEventId, int eventGroupId,
        int? callId = null,
        DateTime? atUtc = null,
        string? description = null
    )
    => New(controllerId, externalEventId, eventGroupId, CallEventKind.Restart, callId,
           routingJson: null, content: null, atUtc: atUtc, severity: DiagSeverity.Info, description: description);
}
