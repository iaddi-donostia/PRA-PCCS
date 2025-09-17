using System.Text.Json;
using System.Collections.Generic;
using PRA.PCCS.Domain.Shared;

namespace PRA.PCCS.Domain.Diag;

public static class FaultEventFactory
{
    // -------- Helpers internos ----------
    private static FaultEvent New(
        int controllerId, int externalEventId, int eventGroupId,
        string typeId, FaultKind kind,
        StructuredData? data = null, int? vendorSeverity = null,
        DateTime? atUtc = null, string? details = null
    )
    => new(controllerId, externalEventId, eventGroupId,
           typeId, kind, data, vendorSeverity, addedAtUtc: atUtc, details: details);

    private static FaultEvent NewJson(
        int controllerId, int externalEventId, int eventGroupId,
        string typeId, FaultKind kind, object payload,
        DateTime? atUtc = null, string? details = null
    )
    => New(controllerId, externalEventId, eventGroupId, typeId, kind,
           new StructuredData(JsonSerializer.Serialize(payload)), null, atUtc, details);

    // -------- 7.4.1 – 7.4.16 --------
    public static FaultEvent AudioPathSupervision(int c, int e, int g, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.AudioPathSupervision, FaultTypes.ToKind(FaultTypes.AudioPathSupervision), atUtc: t, details: d);

    public static FaultEvent MicrophoneSupervision(int c, int e, int g, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.MicrophoneSupervision, FaultTypes.ToKind(FaultTypes.MicrophoneSupervision), atUtc: t, details: d);

    public static FaultEvent ControlInputLineFault(int c, int e, int g, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.ControlInputLineFault, FaultTypes.ToKind(FaultTypes.ControlInputLineFault), atUtc: t, details: d);

    public static FaultEvent CallStationExtension(int c, int e, int g, uint numberConfigured, uint numberDetected, DateTime? t = null)
        => NewJson(c, e, g, FaultTypes.CallStationExtension, FaultTypes.ToKind(FaultTypes.CallStationExtension),
            new { numberConfigured, numberDetected }, t);

    public static FaultEvent ConfigurationFile(int c, int e, int g, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.ConfigurationFile, FaultTypes.ToKind(FaultTypes.ConfigurationFile), atUtc: t, details: d);

    public static FaultEvent ConfigurationVersion(int c, int e, int g, string expected, string loaded, DateTime? t = null)
        => NewJson(c, e, g, FaultTypes.ConfigurationVersion, FaultTypes.ToKind(FaultTypes.ConfigurationVersion),
            new { expected, loaded }, t);

    public static FaultEvent IllegalConfiguration(int c, int e, int g, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.IllegalConfiguration, FaultTypes.ToKind(FaultTypes.IllegalConfiguration), atUtc: t, details: d);

    public static FaultEvent PrerecordedMessagesNames(int c, int e, int g, string missingNamesCsv, DateTime? t = null)
        => NewJson(c, e, g, FaultTypes.PrerecordedMessagesNames, FaultTypes.ToKind(FaultTypes.PrerecordedMessagesNames),
            new { missingNames = missingNamesCsv }, t);

    public static FaultEvent PrerecordedMessagesCorrupt(int c, int e, int g, string corruptNamesCsv, DateTime? t = null)
        => NewJson(c, e, g, FaultTypes.PrerecordedMessagesCorrupt, FaultTypes.ToKind(FaultTypes.PrerecordedMessagesCorrupt),
            new { corruptMessages = corruptNamesCsv }, t);

    public static FaultEvent UnitMissing(int c, int e, int g, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.UnitMissing, FaultTypes.ToKind(FaultTypes.UnitMissing), atUtc: t, details: d);

    public static FaultEvent UnitReset(int c, int e, int g, string chipType, DateTime? t = null)
        => NewJson(c, e, g, FaultTypes.UnitReset, FaultTypes.ToKind(FaultTypes.UnitReset), new { chipType }, t);

    public static FaultEvent UserInjectedFault(int c, int e, int g, string description, DateTime? t = null)
        => New(c, e, g, FaultTypes.UserInjectedFault, FaultTypes.ToKind(FaultTypes.UserInjectedFault), atUtc: t, details: description);

    public static FaultEvent NoFaults(int c, int e, int g, DateTime? t = null)
        => New(c, e, g, FaultTypes.NoFaults, FaultTypes.ToKind(FaultTypes.NoFaults), atUtc: t);

    public static FaultEvent ZoneLineFault(int c, int e, int g, IEnumerable<string> zoneNames, string? controlInputName = null, DateTime? t = null)
        => NewJson(c, e, g, FaultTypes.ZoneLineFault, FaultTypes.ToKind(FaultTypes.ZoneLineFault),
            new { zoneNames = string.Join(",", zoneNames), controlInputName }, t);

    public static FaultEvent NetworkChangeDiagEvent(int c, int e, int g, string localSystemName, string remotePortId, string remoteSystemName, DateTime? t = null)
        => NewJson(c, e, g, FaultTypes.NetworkChangeDiagEvent, FaultTypes.ToKind(FaultTypes.NetworkChangeDiagEvent),
            new { localSystemName, remotePortId, remoteSystemName }, t);

    public static FaultEvent IncompatibleFirmware(int c, int e, int g, string current, string expected, DateTime? t = null)
        => NewJson(c, e, g, FaultTypes.IncompatibleFirmware, FaultTypes.ToKind(FaultTypes.IncompatibleFirmware),
            new { current, expected }, t);

    // -------- 7.4.17 – 7.4.28 (Amplificador, con severidad del fabricante en VendorSeverity) --------
    public static FaultEvent Amp48VAFault(int c, int e, int g, int vendorSeverity, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.Amp48VAFault, FaultTypes.ToKind(FaultTypes.Amp48VAFault), vendorSeverity: vendorSeverity, atUtc: t, details: d);

    public static FaultEvent Amp48VBFault(int c, int e, int g, int vendorSeverity, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.Amp48VBFault, FaultTypes.ToKind(FaultTypes.Amp48VBFault), vendorSeverity: vendorSeverity, atUtc: t, details: d);

    public static FaultEvent AmpChannelFault(int c, int e, int g, int vendorSeverity, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.AmpChannelFault, FaultTypes.ToKind(FaultTypes.AmpChannelFault), vendorSeverity: vendorSeverity, atUtc: t, details: d);

    public static FaultEvent AmpShortCircuitLineAFault(int c, int e, int g, int vendorSeverity, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.AmpShortCircuitLineAFault, FaultTypes.ToKind(FaultTypes.AmpShortCircuitLineAFault), vendorSeverity: vendorSeverity, atUtc: t, details: d);

    public static FaultEvent AmpShortCircuitLineBFault(int c, int e, int g, int vendorSeverity, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.AmpShortCircuitLineBFault, FaultTypes.ToKind(FaultTypes.AmpShortCircuitLineBFault), vendorSeverity: vendorSeverity, atUtc: t, details: d);

    public static FaultEvent AmpAcc18VFault(int c, int e, int g, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.AmpAcc18VFault, FaultTypes.ToKind(FaultTypes.AmpAcc18VFault), atUtc: t, details: d);

    public static FaultEvent AmpSpareInternalFault(int c, int e, int g, int vendorSeverity, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.AmpSpareInternalFault, FaultTypes.ToKind(FaultTypes.AmpSpareInternalFault), vendorSeverity: vendorSeverity, atUtc: t, details: d);

    public static FaultEvent AmpChannelOverloadFault(int c, int e, int g, int vendorSeverity, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.AmpChannelOverloadFault, FaultTypes.ToKind(FaultTypes.AmpChannelOverloadFault), vendorSeverity: vendorSeverity, atUtc: t, details: d);

    public static FaultEvent EolFailureLineAFault(int c, int e, int g, int vendorSeverity, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.EolFailureLineAFault, FaultTypes.ToKind(FaultTypes.EolFailureLineAFault), vendorSeverity: vendorSeverity, atUtc: t, details: d);

    public static FaultEvent EolFailureLineBFault(int c, int e, int g, int vendorSeverity, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.EolFailureLineBFault, FaultTypes.ToKind(FaultTypes.EolFailureLineBFault), vendorSeverity: vendorSeverity, atUtc: t, details: d);

    public static FaultEvent GroundShortFault(int c, int e, int g, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.GroundShortFault, FaultTypes.ToKind(FaultTypes.GroundShortFault), atUtc: t, details: d);

    public static FaultEvent OverheatFault(int c, int e, int g, DateTime? t = null, string? d = null)
        => New(c, e, g, FaultTypes.OverheatFault, FaultTypes.ToKind(FaultTypes.OverheatFault), atUtc: t, details: d);

    // -------- 7.4.29 – 7.4.48 (PSU/ACC/lifeline) --------
    public static FaultEvent PowerMainsSupplyFault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.PowerMainsSupplyFault, FaultTypes.ToKind(FaultTypes.PowerMainsSupplyFault), atUtc: t, details: d);
    public static FaultEvent PowerBackupSupplyFault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.PowerBackupSupplyFault, FaultTypes.ToKind(FaultTypes.PowerBackupSupplyFault), atUtc: t, details: d);
    public static FaultEvent MainsAbsentPSU1Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.MainsAbsentPSU1Fault, FaultTypes.ToKind(FaultTypes.MainsAbsentPSU1Fault), atUtc: t, details: d);
    public static FaultEvent MainsAbsentPSU2Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.MainsAbsentPSU2Fault, FaultTypes.ToKind(FaultTypes.MainsAbsentPSU2Fault), atUtc: t, details: d);
    public static FaultEvent MainsAbsentPSU3Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.MainsAbsentPSU3Fault, FaultTypes.ToKind(FaultTypes.MainsAbsentPSU3Fault), atUtc: t, details: d);
    public static FaultEvent BackupAbsentPSU1Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.BackupAbsentPSU1Fault, FaultTypes.ToKind(FaultTypes.BackupAbsentPSU1Fault), atUtc: t, details: d);
    public static FaultEvent BackupAbsentPSU2Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.BackupAbsentPSU2Fault, FaultTypes.ToKind(FaultTypes.BackupAbsentPSU2Fault), atUtc: t, details: d);
    public static FaultEvent BackupAbsentPSU3Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.BackupAbsentPSU3Fault, FaultTypes.ToKind(FaultTypes.BackupAbsentPSU3Fault), atUtc: t, details: d);
    public static FaultEvent DcOut1PSU1Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.DcOut1PSU1Fault, FaultTypes.ToKind(FaultTypes.DcOut1PSU1Fault), atUtc: t, details: d);
    public static FaultEvent DcOut2PSU1Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.DcOut2PSU1Fault, FaultTypes.ToKind(FaultTypes.DcOut2PSU1Fault), atUtc: t, details: d);
    public static FaultEvent DcOut1PSU2Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.DcOut1PSU2Fault, FaultTypes.ToKind(FaultTypes.DcOut1PSU2Fault), atUtc: t, details: d);
    public static FaultEvent DcOut2PSU2Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.DcOut2PSU2Fault, FaultTypes.ToKind(FaultTypes.DcOut2PSU2Fault), atUtc: t, details: d);
    public static FaultEvent DcOut1PSU3Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.DcOut1PSU3Fault, FaultTypes.ToKind(FaultTypes.DcOut1PSU3Fault), atUtc: t, details: d);
    public static FaultEvent DcOut2PSU3Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.DcOut2PSU3Fault, FaultTypes.ToKind(FaultTypes.DcOut2PSU3Fault), atUtc: t, details: d);
    public static FaultEvent AudioLifelinePSU1Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.AudioLifelinePSU1Fault, FaultTypes.ToKind(FaultTypes.AudioLifelinePSU1Fault), atUtc: t, details: d);
    public static FaultEvent AudioLifelinePSU2Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.AudioLifelinePSU2Fault, FaultTypes.ToKind(FaultTypes.AudioLifelinePSU2Fault), atUtc: t, details: d);
    public static FaultEvent AudioLifelinePSU3Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.AudioLifelinePSU3Fault, FaultTypes.ToKind(FaultTypes.AudioLifelinePSU3Fault), atUtc: t, details: d);
    public static FaultEvent AccSupplyPSU1Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.AccSupplyPSU1Fault, FaultTypes.ToKind(FaultTypes.AccSupplyPSU1Fault), atUtc: t, details: d);
    public static FaultEvent AccSupplyPSU2Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.AccSupplyPSU2Fault, FaultTypes.ToKind(FaultTypes.AccSupplyPSU2Fault), atUtc: t, details: d);
    public static FaultEvent AccSupplyPSU3Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.AccSupplyPSU3Fault, FaultTypes.ToKind(FaultTypes.AccSupplyPSU3Fault), atUtc: t, details: d);

    // -------- 7.4.49 – 7.4.68 (ventiladores / DC Aux / batería / PoE / PSU A-B / varios) --------
    public static FaultEvent Fan1Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.Fan1Fault, FaultTypes.ToKind(FaultTypes.Fan1Fault), atUtc: t, details: d);
    public static FaultEvent Fan2Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.Fan2Fault, FaultTypes.ToKind(FaultTypes.Fan2Fault), atUtc: t, details: d);
    public static FaultEvent DcAux1Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.DcAux1Fault, FaultTypes.ToKind(FaultTypes.DcAux1Fault), atUtc: t, details: d);
    public static FaultEvent DcAux2Fault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.DcAux2Fault, FaultTypes.ToKind(FaultTypes.DcAux2Fault), atUtc: t, details: d);
    public static FaultEvent BatteryShortFault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.BatteryShortFault, FaultTypes.ToKind(FaultTypes.BatteryShortFault), atUtc: t, details: d);
    public static FaultEvent BatteryRiFault(int c, int e, int g, double? internalResistanceMilliOhm = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.BatteryRiFault, FaultTypes.ToKind(FaultTypes.BatteryRiFault), new { internalResistanceMilliOhm }, t, d);
    public static FaultEvent BatteryOverheatFault(int c, int e, int g, double? temperatureC = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.BatteryOverheatFault, FaultTypes.ToKind(FaultTypes.BatteryOverheatFault), new { temperatureC }, t, d);
    public static FaultEvent BatteryFloatChargeFault(int c, int e, int g, double? voltageV = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.BatteryFloatChargeFault, FaultTypes.ToKind(FaultTypes.BatteryFloatChargeFault), new { voltageV }, t, d);
    public static FaultEvent MainsAbsentChargerFault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.MainsAbsentChargerFault, FaultTypes.ToKind(FaultTypes.MainsAbsentChargerFault), atUtc: t, details: d);
    public static FaultEvent PoESupplyFault(int c, int e, int g, string? portId = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.PoESupplyFault, FaultTypes.ToKind(FaultTypes.PoESupplyFault), new { portId }, t, d);
    public static FaultEvent PowerSupplyAFault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.PowerSupplyAFault, FaultTypes.ToKind(FaultTypes.PowerSupplyAFault), atUtc: t, details: d);
    public static FaultEvent PowerSupplyBFault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.PowerSupplyBFault, FaultTypes.ToKind(FaultTypes.PowerSupplyBFault), atUtc: t, details: d);
    public static FaultEvent ExternalPowerFault(int c, int e, int g, double? voltageV = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.ExternalPowerFault, FaultTypes.ToKind(FaultTypes.ExternalPowerFault), new { voltageV }, t, d);
    public static FaultEvent ChargerSupplyVoltageTooLow(int c, int e, int g, double? voltageV = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.ChargerSupplyVoltageTooLowFault, FaultTypes.ToKind(FaultTypes.ChargerSupplyVoltageTooLowFault), new { voltageV }, t, d);
    public static FaultEvent BatteryOvervoltageFault(int c, int e, int g, double? voltageV = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.BatteryOvervoltageFault, FaultTypes.ToKind(FaultTypes.BatteryOvervoltageFault), new { voltageV }, t, d);
    public static FaultEvent BatteryUndervoltageFault(int c, int e, int g, double? voltageV = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.BatteryUndervoltageFault, FaultTypes.ToKind(FaultTypes.BatteryUndervoltageFault), new { voltageV }, t, d);
    public static FaultEvent MediaClockFault(int c, int e, int g, double? ppmDrift = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.MediaClockFault, FaultTypes.ToKind(FaultTypes.MediaClockFault), new { ppmDrift }, t, d);
    public static FaultEvent ChargerFault(int c, int e, int g, string? phase = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.ChargerFault, FaultTypes.ToKind(FaultTypes.ChargerFault), new { phase }, t, d);
    public static FaultEvent Amp20VFault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.Amp20VFault, FaultTypes.ToKind(FaultTypes.Amp20VFault), atUtc: t, details: d);
    public static FaultEvent AmpPsuFault(int c, int e, int g, DateTime? t = null, string? d = null) => New(c, e, g, FaultTypes.AmpPsuFault, FaultTypes.ToKind(FaultTypes.AmpPsuFault), atUtc: t, details: d);

    // -------- 7.4.69 – 7.4.86 (network/sync/audio delay/internal/remote/license/stacking) --------
    public static FaultEvent NetworkLatencyFault(int c, int e, int g, int? latencyMs = null, string? path = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.NetworkLatencyFault, FaultTypes.ToKind(FaultTypes.NetworkLatencyFault), new { latencyMs, path }, t, d);

    public static FaultEvent SynchronizationFault(int c, int e, int g, string? domain = null, string? node = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.SynchronizationFault, FaultTypes.ToKind(FaultTypes.SynchronizationFault), new { domain, node }, t, d);

    public static FaultEvent AudioDelayFault(int c, int e, int g, int? delayMs = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.AudioDelayFault, FaultTypes.ToKind(FaultTypes.AudioDelayFault), new { delayMs }, t, d);

    public static FaultEvent InternalPowerFault(int c, int e, int g, string? rail = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.InternalPowerFault, FaultTypes.ToKind(FaultTypes.InternalPowerFault), new { rail }, t, d);

    public static FaultEvent InternalCommunicationFault(int c, int e, int g, string? board = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.InternalCommunicationFault, FaultTypes.ToKind(FaultTypes.InternalCommunicationFault), new { board }, t, d);

    public static FaultEvent VoipFault(int c, int e, int g, string? d = null, DateTime? t = null)
        => New(c, e, g, FaultTypes.VoIPFault, FaultTypes.ToKind(FaultTypes.VoIPFault), atUtc: t, details: d);

    public static FaultEvent RemoteOutputFault(int c, int e, int g, string? remoteZoneGroupName = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.RemoteOutputFault, FaultTypes.ToKind(FaultTypes.RemoteOutputFault), new { remoteZoneGroupName }, t, d);

    public static FaultEvent RemoteOutputLoopFault(int c, int e, int g, string? remoteZoneGroupName = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.RemoteOutputLoopFault, FaultTypes.ToKind(FaultTypes.RemoteOutputLoopFault), new { remoteZoneGroupName }, t, d);

    public static FaultEvent RemoteOutputConfiguration(int c, int e, int g, string? remoteZoneGroupName = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.RemoteOutputConfigurationFault, FaultTypes.ToKind(FaultTypes.RemoteOutputConfigurationFault), new { remoteZoneGroupName }, t, d);

    public static FaultEvent RemoteSystemFault(int c, int e, int g, string? remoteSystemName = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.RemoteSystemFault, FaultTypes.ToKind(FaultTypes.RemoteSystemFault), new { remoteSystemName }, t, d);

    public static FaultEvent RemoteMainPowerFault(int c, int e, int g, string? remoteSystemName = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.RemoteMainPowerFault, FaultTypes.ToKind(FaultTypes.RemoteMainPowerFault), new { remoteSystemName }, t, d);

    public static FaultEvent RemoteBackupPowerFault(int c, int e, int g, string? remoteSystemName = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.RemoteBackupPowerFault, FaultTypes.ToKind(FaultTypes.RemoteBackupPowerFault), new { remoteSystemName }, t, d);

    public static FaultEvent RemoteGroundFault(int c, int e, int g, string? remoteSystemName = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.RemoteGroundFault, FaultTypes.ToKind(FaultTypes.RemoteGroundFault), new { remoteSystemName }, t, d);

    public static FaultEvent RemoteFault(int c, int e, int g, string? remoteSystemName = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.RemoteFault, FaultTypes.ToKind(FaultTypes.RemoteFault), new { remoteSystemName }, t, d);

    public static FaultEvent PowerSupplyFault(int c, int e, int g, string? supplyName = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.PowerSupplyFault, FaultTypes.ToKind(FaultTypes.PowerSupplyFault), new { supplyName }, t, d);

    public static FaultEvent StackedSwitchMismatchFault(int c, int e, int g, string? stackInfo = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.StackedSwitchMismatchFault, FaultTypes.ToKind(FaultTypes.StackedSwitchMismatchFault), new { stackInfo }, t, d);

    public static FaultEvent RedundantDataPathFault(int c, int e, int g, string? pathInfo = null, DateTime? t = null, string? d = null)
        => NewJson(c, e, g, FaultTypes.RedundantDataPathFault, FaultTypes.ToKind(FaultTypes.RedundantDataPathFault), new { pathInfo }, t, d);
}

