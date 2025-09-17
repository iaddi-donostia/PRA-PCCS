namespace PRA.PCCS.Domain.Diag;

public static class FaultTypes
{
    // 7.4.1 – 7.4.16 (varios / config / general)
    public const string AudioPathSupervision = "DET_AudioPathSupervision";
    public const string MicrophoneSupervision = "DET_MicrophoneSupervision";
    public const string ControlInputLineFault = "DET_ControlInputLineFault";
    public const string CallStationExtension = "DET_CallStationExtension";
    public const string ConfigurationFile = "DET_ConfigurationFile";
    public const string ConfigurationVersion = "DET_ConfigurationVersion";
    public const string IllegalConfiguration = "DET_IllegalConfiguration";
    public const string PrerecordedMessagesNames = "DET_PrerecordedMessagesNames";
    public const string PrerecordedMessagesCorrupt = "DET_PrerecordedMessagesCorrupt";
    public const string UnitMissing = "DET_UnitMissing";
    public const string UnitReset = "DET_UnitReset";
    public const string UserInjectedFault = "DET_UserInjectedFault";
    public const string NoFaults = "DET_NoFaults";
    public const string ZoneLineFault = "DET_ZoneLineFault";
    public const string NetworkChangeDiagEvent = "DET_NetworkChangeDiagEvent";
    public const string IncompatibleFirmware = "DET_IncompatibleFirmware";

    // 7.4.17 – 7.4.28 (Amplificador)
    public const string Amp48VAFault = "DET_Amp48VAFault";
    public const string Amp48VBFault = "DET_Amp48VBFault";
    public const string AmpChannelFault = "DET_AmpChannelFault";
    public const string AmpShortCircuitLineAFault = "DET_AmpShortCircuitLineAFault";
    public const string AmpShortCircuitLineBFault = "DET_AmpShortCircuitLineBFault";
    public const string AmpAcc18VFault = "DET_AmpAcc18VFault";
    public const string AmpSpareInternalFault = "DET_AmpSpareInternalFault";
    public const string AmpChannelOverloadFault = "DET_AmpChannelOverloadFault";
    public const string EolFailureLineAFault = "DET_EolFailureLineAFault";
    public const string EolFailureLineBFault = "DET_EolFailureLineBFault";
    public const string GroundShortFault = "DET_GroundShortFault";
    public const string OverheatFault = "DET_OverheatFault";

    // 7.4.29 – 7.4.48 (PSU / ACC / lifeline)
    public const string PowerMainsSupplyFault = "DET_PowerMainsSupplyFault";
    public const string PowerBackupSupplyFault = "DET_PowerBackupSupplyFault";
    public const string MainsAbsentPSU1Fault = "DET_MainsAbsentPSU1Fault";
    public const string MainsAbsentPSU2Fault = "DET_MainsAbsentPSU2Fault";
    public const string MainsAbsentPSU3Fault = "DET_MainsAbsentPSU3Fault";
    public const string BackupAbsentPSU1Fault = "DET_BackupAbsentPSU1Fault";
    public const string BackupAbsentPSU2Fault = "DET_BackupAbsentPSU2Fault";
    public const string BackupAbsentPSU3Fault = "DET_BackupAbsentPSU3Fault";
    public const string DcOut1PSU1Fault = "DET_DcOut1PSU1Fault";
    public const string DcOut2PSU1Fault = "DET_DcOut2PSU1Fault";
    public const string DcOut1PSU2Fault = "DET_DcOut1PSU2Fault";
    public const string DcOut2PSU2Fault = "DET_DcOut2PSU2Fault";
    public const string DcOut1PSU3Fault = "DET_DcOut1PSU3Fault";
    public const string DcOut2PSU3Fault = "DET_DcOut2PSU3Fault";
    public const string AudioLifelinePSU1Fault = "DET_AudioLifelinePSU1Fault";
    public const string AudioLifelinePSU2Fault = "DET_AudioLifelinePSU2Fault";
    public const string AudioLifelinePSU3Fault = "DET_AudioLifelinePSU3Fault";
    public const string AccSupplyPSU1Fault = "DET_AccSupplyPSU1Fault";
    public const string AccSupplyPSU2Fault = "DET_AccSupplyPSU2Fault";
    public const string AccSupplyPSU3Fault = "DET_AccSupplyPSU3Fault";

    // 7.4.49 – 7.4.68 (fans, DC Aux, batería/charger/PoE, PSU A/B, varios)
    public const string Fan1Fault = "DET_Fan1Fault";
    public const string Fan2Fault = "DET_Fan2Fault";
    public const string DcAux1Fault = "DET_DcAux1Fault";
    public const string DcAux2Fault = "DET_DcAux2Fault";
    public const string BatteryShortFault = "DET_BatteryShortFault";
    public const string BatteryRiFault = "DET_BatteryRiFault";
    public const string BatteryOverheatFault = "DET_BatteryOverheatFault";
    public const string BatteryFloatChargeFault = "DET_BatteryFloatChargeFault";
    public const string MainsAbsentChargerFault = "DET_MainsAbsentChargerFault";
    public const string PoESupplyFault = "DET_PoESupplyFault";
    public const string PowerSupplyAFault = "DET_PowerSupplyAFault";
    public const string PowerSupplyBFault = "DET_PowerSupplyBFault";
    public const string ExternalPowerFault = "DET_ExternalPowerFault";
    public const string ChargerSupplyVoltageTooLowFault = "DET_ChargerSupplyVoltageTooLowFault";
    public const string BatteryOvervoltageFault = "DET_BatteryOvervoltageFault";
    public const string BatteryUndervoltageFault = "DET_BatteryUndervoltageFault";
    public const string MediaClockFault = "DET_MediaClockFault";
    public const string ChargerFault = "DET_ChargerFault";
    public const string Amp20VFault = "DET_Amp20VFault";
    public const string AmpPsuFault = "DET_AmpPsuFault";

    // 7.4.69 – 7.4.86 (network/sync/audio delay/internal/remote/license/stacking)
    public const string NetworkLatencyFault = "DET_NetworkLatencyFault";
    public const string SynchronizationFault = "DET_SynchronizationFault"; // (aparece también con un typo en algunos textos)
    public const string AudioDelayFault = "DET_AudioDelayFault";
    public const string InternalPowerFault = "DET_InternalPowerFault";
    public const string InternalCommunicationFault = "DET_InternalCommunicationFault";
    public const string VoIPFault = "DET_VoIPFault";
    public const string VoipFault = VoIPFault;       // alias para llamadas que usen 'Voip' Es por fallos de ortografia en el manual del OPen Interface
    public const string RemoteOutputFault = "DET_RemoteOutputFault";
    public const string RemoteOutputLoopFault = "DET_RemoteOutputLoopFault";
    public const string RemoteOutputConfigurationFault = "DET_RemoteOutputConfigurationFault";
    public const string LicenseFault = "DET_LicenseFault";
    public const string RemoteSystemFault = "DET_RemoteSystemFault";
    public const string RemoteMainPowerFault = "DET_RemoteMainPowerFault";
    public const string RemoteBackupPowerFault = "DET_RemoteBackupPowerFault";
    public const string RemoteGroundFault = "DET_RemoteGroundFault";
    public const string RemoteFault = "DET_RemoteFault";
    public const string PowerSupplyFault = "DET_PowerSupplyFault";
    public const string StackedSwitchMismatchFault = "DET_StackedSwitchMismatchFault";
    public const string RedundantDataPathFault = "DET_RedundantDataPathFault";

    // ---- Mapeo a FaultKind (bucket interno para agregación rápida) ----
    public static FaultKind ToKind(string typeId) => typeId switch
    {
        // Amplificador
        Amp48VAFault or Amp48VBFault or AmpChannelFault or AmpShortCircuitLineAFault or AmpShortCircuitLineBFault
            or AmpAcc18VFault or AmpSpareInternalFault or AmpChannelOverloadFault or EolFailureLineAFault
            or EolFailureLineBFault or GroundShortFault or OverheatFault or Amp20VFault or AmpPsuFault
            => FaultKind.AmpFault,

        // Fuente/PSU/PoE/DC Aux/Externa
        PowerMainsSupplyFault or PowerBackupSupplyFault or MainsAbsentPSU1Fault or MainsAbsentPSU2Fault or MainsAbsentPSU3Fault
            or BackupAbsentPSU1Fault or BackupAbsentPSU2Fault or BackupAbsentPSU3Fault
            or DcOut1PSU1Fault or DcOut2PSU1Fault or DcOut1PSU2Fault or DcOut2PSU2Fault
            or DcOut1PSU3Fault or DcOut2PSU3Fault or AudioLifelinePSU1Fault or AudioLifelinePSU2Fault
            or AudioLifelinePSU3Fault or PowerSupplyAFault or PowerSupplyBFault or ExternalPowerFault
            or PoESupplyFault or PowerSupplyFault or DcAux1Fault or DcAux2Fault or InternalPowerFault
            => FaultKind.PowerSupply,

        // Batería / Cargador
        BatteryShortFault or BatteryRiFault or BatteryOverheatFault or BatteryFloatChargeFault
            or MainsAbsentChargerFault or ChargerSupplyVoltageTooLowFault or BatteryOvervoltageFault
            or BatteryUndervoltageFault or ChargerFault
            => FaultKind.BatteryOrCharger,

        // Red / remoto
        NetworkChangeDiagEvent or NetworkLatencyFault or RemoteSystemFault or RemoteMainPowerFault
            or RemoteBackupPowerFault or RemoteGroundFault or RemoteFault or RedundantDataPathFault
            or StackedSwitchMismatchFault
            => FaultKind.Network,

        // Sincronización / retardo
        SynchronizationFault or AudioDelayFault or MediaClockFault
            => FaultKind.Synchronization,

        // Salidas remotas
        RemoteOutputFault or RemoteOutputLoopFault or RemoteOutputConfigurationFault
            => FaultKind.RemoteOutput,

        // Configuración / catálogo / ficheros
        ConfigurationFile or ConfigurationVersion or IllegalConfiguration or PrerecordedMessagesNames
            or PrerecordedMessagesCorrupt or CallStationExtension or UserInjectedFault
            => FaultKind.Configuration,

        // Zonas
        ZoneLineFault => FaultKind.ZoneLine,

        // Otros específicos
        VoIPFault => FaultKind.VoIP,

        // Resto (hardware/miscelánea)
        AudioPathSupervision or MicrophoneSupervision or ControlInputLineFault
            or UnitMissing or UnitReset or NoFaults or IncompatibleFirmware
            => FaultKind.Other,

        _ => FaultKind.Unknown
    };
}

