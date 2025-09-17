namespace PRA.PCCS.Domain.Controllers;

public sealed class Chime
{
    public int Id { get; private set; }               // antes: ChimeId
    public string Name { get; private set; } = string.Empty; // antes: ChimeName (no null)
    public bool IsObsolete { get; private set; }      // antes: Obsolete

    public int ControllerId { get; private set; }     // FK

    private Chime() { } // EF

    public Chime(string name, int controllerId)
    {
        Rename(name);
        ControllerId = controllerId;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Chime name required.", nameof(name));
        Name = name.Trim();
    }

    public void SetObsolete(bool value) => IsObsolete = value;
}

