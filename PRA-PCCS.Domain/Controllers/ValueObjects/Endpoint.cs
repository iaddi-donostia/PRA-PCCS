namespace PRA.PCCS.Domain.Controllers.ValueObjects;

public sealed class Endpoint
{
    public string Host { get; private set; } = string.Empty;
    public int Port { get; private set; } = 0;
    public bool UseTls { get; private set; }

    private Endpoint() { } // EF

    public Endpoint(string host, int port, bool useTls)
    {
        SetHost(host);
        SetPort(port);
        UseTls = useTls;
    }

    public static Endpoint Empty => new("", 0, false);

    public void SetHost(string host)
    {
        if (string.IsNullOrWhiteSpace(host)) throw new ArgumentException("Host required.", nameof(host));
        Host = host.Trim();
    }

    public void SetPort(int port)
    {
        if (port < 0 || port > 65535) throw new ArgumentOutOfRangeException(nameof(port));
        Port = port;
    }

    public void SetTls(bool useTls) => UseTls = useTls;
}

