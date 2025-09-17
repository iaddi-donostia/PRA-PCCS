using PRA.PCCS.Domain.Controllers.ValueObjects;
using PRA.PCCS.Domain.Diag;

namespace PRA.PCCS.Domain.Controllers;

public sealed class Controller
{
    // PK por convención EF
    public int Id { get; private set; }

    // Datos descriptivos
    public string? LocalName { get; private set; }
    public string? Location { get; private set; }
    public string? ControllerType { get; private set; }

    // Conectividad y credenciales (VOs)
    public Endpoint Endpoint { get; private set; } = Endpoint.Empty;
    public Credentials Credentials { get; private set; } = Credentials.Empty;

    // Estado
    public bool IsActive { get; private set; }
    public bool IsConnected { get; private set; }
    public bool? HasAtLeastOneConnection { get; private set; }

    // Versionado / Config
    public string? NcoVersion { get; private set; }
    public string? ProtocolVersion { get; private set; }
    public int? VersionMajor { get; private set; }
    public int? VersionMinor { get; private set; }
    public uint? ConfigId { get; private set; }

    // Métricas
    public int? ConfiguredUnitsCount { get; private set; }
    public int? ConnectedUnitsCount { get; private set; }

    // === Colecciones (backing fields) ===
    private readonly List<Zone> _zones = new();
    public IReadOnlyList<Zone> Zones => _zones;

    private readonly List<ZoneGroup> _zoneGroups = new();
    public IReadOnlyList<ZoneGroup> Groups => _zoneGroups;

    private readonly List<AudioInput> _audioInputs = new();
    public IReadOnlyList<AudioInput> AudioInputs => _audioInputs;

    private readonly List<Bgm> _bgms = new();
    public IReadOnlyList<Bgm> BGMs => _bgms;

    private readonly List<Message> _messages = new();
    public IReadOnlyList<Message> Messages => _messages;

    private readonly List<Chime> _chimes = new();
    public IReadOnlyList<Chime> Chimes => _chimes;

    private readonly List<ConfiguredUnit> _configuredUnits = new();
    public IReadOnlyList<ConfiguredUnit> ConfiguredUnits => _configuredUnits;

    private readonly List<DiagEvent> _diagEvents = new();
    public IReadOnlyList<DiagEvent> DE_DiagEvents => _diagEvents;

    // EF necesita ctor sin parámetros (puede ser private o protected)
    private Controller() { }

    public Controller(Endpoint endpoint, Credentials credentials, bool isActive = false)
    {
        ChangeEndpoint(endpoint);
        ChangeCredentials(credentials);
        SetActive(isActive);
    }

    // --- Intentions / invariantes mínimas ---
    public void Rename(string? localName) => LocalName = string.IsNullOrWhiteSpace(localName) ? null : localName.Trim();
    public void Relocate(string? location) => Location = string.IsNullOrWhiteSpace(location) ? null : location.Trim();
    public void SetControllerType(string? type) => ControllerType = string.IsNullOrWhiteSpace(type) ? null : type.Trim();

    public void ChangeEndpoint(Endpoint endpoint) =>
        Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));

    public void ChangeCredentials(Credentials creds) =>
        Credentials = creds ?? throw new ArgumentNullException(nameof(creds));

    public void SetActive(bool active) => IsActive = active;
    public void SetConnected(bool connected) => IsConnected = connected;

    public void SetProtocolInfo(string? nco, string? protocol, int? major, int? minor)
    {
        NcoVersion = string.IsNullOrWhiteSpace(nco) ? null : nco.Trim();
        ProtocolVersion = string.IsNullOrWhiteSpace(protocol) ? null : protocol.Trim();
        VersionMajor = major;
        VersionMinor = minor;
    }

    public void SetConfigId(uint? configId) => ConfigId = configId;

    public void SetUnitCounts(int? configured, int? connected, bool? hasAny)
    {
        ConfiguredUnitsCount = configured;
        ConnectedUnitsCount = connected;
        HasAtLeastOneConnection = hasAny;
    }

    // === Zones ===
    public Zone AddZone(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Zone name required.", nameof(name));
        var z = new Zone(name.Trim(), Id);
        _zones.Add(z);
        return z;
    }

    public void AttachZone(Zone zone)
    {
        if (zone is null) throw new ArgumentNullException(nameof(zone));
        if (zone.ControllerId != Id) throw new InvalidOperationException("Zone.ControllerId must match Controller.Id.");
        _zones.Add(zone);
    }

    public bool RemoveZone(int zoneId)
    {
        var z = _zones.FirstOrDefault(x => x.Id == zoneId);
        if (z is null) return false;
        _zones.Remove(z);
        return true;
    }

    public Zone? FindZoneByName(string name) =>
        _zones.FirstOrDefault(z => string.Equals(z.Name, name, StringComparison.OrdinalIgnoreCase));

    // === ZoneGroups ===
    public ZoneGroup AddZoneGroup(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Group name required.", nameof(name));
        var g = new ZoneGroup(name.Trim(), Id);
        _zoneGroups.Add(g);
        return g;
    }

    public void AttachZoneGroup(ZoneGroup group)
    {
        if (group is null) throw new ArgumentNullException(nameof(group));
        if (group.ControllerId != Id) throw new InvalidOperationException("ZoneGroup.ControllerId must match Controller.Id.");
        _zoneGroups.Add(group);
    }

    public bool RemoveZoneGroup(int groupId)
    {
        var g = _zoneGroups.FirstOrDefault(x => x.Id == groupId);
        if (g is null) return false;
        _zoneGroups.Remove(g);
        return true;
    }

    public ZoneGroup? FindGroupByName(string name) =>
        _zoneGroups.FirstOrDefault(g => string.Equals(g.Name, name, StringComparison.OrdinalIgnoreCase));

    public AudioInput AddAudioInput(string? name)
    {
        var ai = new AudioInput(name, Id);
        _audioInputs.Add(ai);
        return ai;
    }

    public void AttachAudioInput(AudioInput ai)
    {
        if (ai is null) throw new ArgumentNullException(nameof(ai));
        if (ai.ControllerId != Id) throw new InvalidOperationException("AudioInput.ControllerId must match Controller.Id.");
        _audioInputs.Add(ai);
    }

    public bool RemoveAudioInput(int audioInputId)
    {
        var ai = _audioInputs.FirstOrDefault(x => x.Id == audioInputId);
        if (ai is null) return false;
        _audioInputs.Remove(ai);
        return true;
    }
    public Bgm AddBgm(string? name)
    {
        var bgm = new Bgm(name, Id);
        _bgms.Add(bgm);
        return bgm;
    }

    public void AttachBgm(Bgm bgm)
    {
        if (bgm is null) throw new ArgumentNullException(nameof(bgm));
        if (bgm.ControllerId != Id) throw new InvalidOperationException("Bgm.ControllerId must match Controller.Id.");
        _bgms.Add(bgm);
    }

    public bool RemoveBgm(int bgmId)
    {
        var b = _bgms.FirstOrDefault(x => x.Id == bgmId);
        if (b is null) return false;
        _bgms.Remove(b);
        return true;
    }

    public Message AddMessage(string name)
    {
        var m = new Message(name, Id);
        _messages.Add(m);
        return m;
    }

    public void AttachMessage(Message message)
    {
        if (message is null) throw new ArgumentNullException(nameof(message));
        if (message.ControllerId != Id) throw new InvalidOperationException("Message.ControllerId must match Controller.Id.");
        _messages.Add(message);
    }

    public bool RemoveMessage(int messageId)
    {
        var m = _messages.FirstOrDefault(x => x.Id == messageId);
        if (m is null) return false;
        _messages.Remove(m);
        return true;
    }

    public Message? FindMessageByName(string name) =>
        _messages.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));

    public Chime AddChime(string name)
    {
        var ch = new Chime(name, Id);
        _chimes.Add(ch);
        return ch;
    }

    public void AttachChime(Chime chime)
    {
        if (chime is null) throw new ArgumentNullException(nameof(chime));
        if (chime.ControllerId != Id) throw new InvalidOperationException("Chime.ControllerId must match Controller.Id.");
        _chimes.Add(chime);
    }

    public bool RemoveChime(int chimeId)
    {
        var ch = _chimes.FirstOrDefault(x => x.Id == chimeId);
        if (ch is null) return false;
        _chimes.Remove(ch);
        return true;
    }

    public Chime? FindChimeByName(string name) =>
        _chimes.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));

    public ConfiguredUnit AddConfiguredUnit(string name, string host, string? description = null)
    {
        var cu = new ConfiguredUnit(name, host, Id, description);
        _configuredUnits.Add(cu);
        return cu;
    }

    public void AttachConfiguredUnit(ConfiguredUnit cu)
    {
        if (cu is null) throw new ArgumentNullException(nameof(cu));
        if (cu.ControllerId != Id) throw new InvalidOperationException("ConfiguredUnit.ControllerId must match Controller.Id.");
        _configuredUnits.Add(cu);
    }

    public bool RemoveConfiguredUnit(int configuredUnitId)
    {
        var cu = _configuredUnits.FirstOrDefault(x => x.Id == configuredUnitId);
        if (cu is null) return false;
        _configuredUnits.Remove(cu);
        return true;
    }

    public ConfiguredUnit? FindConfiguredUnitByName(string name) =>
        _configuredUnits.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
    public T AttachDiagEvent<T>(T ev) where T : DiagEvent
    {
        if (ev is null) throw new ArgumentNullException(nameof(ev));
        if (ev.ControllerId != Id) throw new InvalidOperationException("DiagEvent.ControllerId must match Controller.Id.");
        _diagEvents.Add(ev);
        return ev;
    }

    public bool RemoveDiagEvent(int diagEventId)
    {
        var ev = _diagEvents.FirstOrDefault(x => x.Id == diagEventId);
        if (ev is null) return false;
        _diagEvents.Remove(ev);
        return true;
    }
}

