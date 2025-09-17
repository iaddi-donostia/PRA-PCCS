namespace PRA.PCCS.Domain.Controllers;

public sealed class Message
{
    public int Id { get; private set; }                 // antes: MessageId
    public string Name { get; private set; } = string.Empty; // antes: MessageName (no null en tu modelo)
    public bool IsObsolete { get; private set; }        // antes: Obsolete

    // Relación con Controller
    public int ControllerId { get; private set; }

    private Message() { } // EF

    public Message(string name, int controllerId)
    {
        Rename(name);
        ControllerId = controllerId;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Message name required.", nameof(name));

        Name = name.Trim();
    }

    public void SetObsolete(bool value) => IsObsolete = value;
}
