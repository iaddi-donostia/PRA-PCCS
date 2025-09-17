using System.Linq;
using PRA.PCCS.Domain.Shared;

namespace PRA.PCCS.Domain.Diag;

/// <summary>
/// Fábrica de FaultEvent para evitar strings sueltas y unificar defaults.
/// </summary>
public static class FaultEvents
{
    public static FaultEvent AmpFaultLow(int controllerId, int externalEventId, int eventGroupId,
        DateTime? atUtc = null, string? unitName = null, string? details = null) =>
        new(controllerId, externalEventId, eventGroupId,
            FaultTypes.Amp48VAFault, FaultTypes.ToKind(FaultTypes.Amp48VAFault),
            vendorSeverity: 0, addedAtUtc: atUtc, unitName: unitName, details: details);

    public static FaultEvent AmpFaultHigh(int controllerId, int externalEventId, int eventGroupId,
        DateTime? atUtc = null, string? unitName = null, string? details = null) =>
        new(controllerId, externalEventId, eventGroupId,
            FaultTypes.Amp48VAFault, FaultTypes.ToKind(FaultTypes.Amp48VAFault),
            vendorSeverity: 1, addedAtUtc: atUtc, unitName: unitName, details: details);

    public static FaultEvent NetworkChange(int controllerId, int externalEventId, int eventGroupId,
        string networkChangesJson, DateTime? atUtc = null, string? details = null) =>
        new(controllerId, externalEventId, eventGroupId,
            FaultTypes.NetworkChangeDiagEvent, FaultTypes.ToKind(FaultTypes.NetworkChangeDiagEvent),
            data: new StructuredData(SafeJson(networkChangesJson)), addedAtUtc: atUtc, details: details);

    public static FaultEvent ZoneLineFault(int controllerId, int externalEventId, int eventGroupId,
        IEnumerable<string> zoneNames, string? controlInputName = null, DateTime? atUtc = null)
    {
        var names = string.Join(",", zoneNames.Select(z => z.Replace("\"", "\\\"")));
        var ci = controlInputName is null ? "null" : $"\"{controlInputName.Replace("\"", "\\\"")}\"";
        var json = $"{{\"zoneNames\":\"{names}\",\"controlInputName\":{ci}}}";
        return new(controllerId, externalEventId, eventGroupId,
            FaultTypes.ZoneLineFault, FaultTypes.ToKind(FaultTypes.ZoneLineFault),
            data: new StructuredData(json), addedAtUtc: atUtc);
    }

    public static FaultEvent RemoteOutputLoopFault(int controllerId, int externalEventId, int eventGroupId,
        string remoteZoneGroupName, DateTime? atUtc = null)
    {
        var json = $"{{\"remoteZoneGroupName\":\"{remoteZoneGroupName.Replace("\"", "\\\"")}\"}}";
        return new(controllerId, externalEventId, eventGroupId,
            FaultTypes.RemoteOutputLoopFault, FaultTypes.ToKind(FaultTypes.RemoteOutputLoopFault),
            data: new StructuredData(json), addedAtUtc: atUtc);
    }

    public static FaultEvent IncompatibleFirmware(int controllerId, int externalEventId, int eventGroupId,
        string current, string expected, DateTime? atUtc = null)
    {
        var json = $"{{\"current\":\"{current.Replace("\"", "\\\"")}\",\"expected\":\"{expected.Replace("\"", "\\\"")}\"}}";
        return new(controllerId, externalEventId, eventGroupId,
            FaultTypes.IncompatibleFirmware, FaultTypes.ToKind(FaultTypes.IncompatibleFirmware),
            data: new StructuredData(json), addedAtUtc: atUtc);
    }

    public static FaultEvent LicenseFault(int controllerId, int externalEventId, int eventGroupId,
        string? licenseName = null, DateTime? atUtc = null, string? details = null)
    {
        var json = licenseName is null ? "{}" : $"{{\"licenseName\":\"{licenseName.Replace("\"", "\\\"")}\"}}";
        return new(controllerId, externalEventId, eventGroupId,
            FaultTypes.LicenseFault, FaultTypes.ToKind(FaultTypes.LicenseFault),
            data: new StructuredData(json), addedAtUtc: atUtc, details: details);
    }

    public static FaultEvent VoipFault(int controllerId, int externalEventId, int eventGroupId,
        string? details = null, DateTime? atUtc = null) =>
        new(controllerId, externalEventId, eventGroupId,
            FaultTypes.VoipFault, FaultTypes.ToKind(FaultTypes.VoipFault),
            addedAtUtc: atUtc, details: details);

    private static string SafeJson(string? json) => string.IsNullOrWhiteSpace(json) ? "{}" : json.Trim();
}
