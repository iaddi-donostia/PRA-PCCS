namespace PRA.PCCS.Domain.Controllers;

public sealed class Bgm
{
    public int Id { get; private set; }              // antes: BGMId
    public string? Name { get; private set; }        // antes: BGMName (se mantiene nullable)
    public bool IsObsolete { get; private set; }     // antes: Obsolete

    // Relación con Controller
    public int ControllerId { get; private set; }

    // EF
    private Bgm() { }

    public Bgm(string? name, int controllerId)
    {
        Rename(name);
        ControllerId = controllerId;
    }

    public void Rename(string? name)
    {
        // Mantiene la posibilidad de null como en el modelo original
        Name = string.IsNullOrWhiteSpace(name) ? null : name.Trim();
    }

    public void SetObsolete(bool value) => IsObsolete = value;
}

