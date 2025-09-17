namespace PRA.PCCS.Domain.Controllers.ValueObjects;

public sealed class Credentials
{
    public string User { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;

    // Requerido por EF
    private Credentials() { }

    public Credentials(string user, string password)
    {
        SetUser(user);
        SetPassword(password);
    }

    public static Credentials Empty => new("", "");

    public void SetUser(string user)
    {
        if (string.IsNullOrWhiteSpace(user)) throw new ArgumentException("User required.", nameof(user));
        User = user.Trim();
    }

    public void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password required.", nameof(password));
        Password = password.Trim();
    }
}
