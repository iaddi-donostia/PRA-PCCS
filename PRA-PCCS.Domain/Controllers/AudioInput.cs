namespace PRA.PCCS.Domain.Controllers;

public sealed class AudioInput
{
    public int Id { get; private set; }                     // antes: AudioInputId
    public string? Name { get; private set; }               // antes: AudioInputName (seguimos permitiendo null si así venía)
    public bool IsObsolete { get; private set; }            // antes: Obsolete

    // Relación con Controller
    public int ControllerId { get; private set; }

    // Ctor requerido por EF
    private AudioInput() { }

    public AudioInput(string? name, int controllerId)
    {
        Rename(name);
        ControllerId = controllerId;
    }

    public void Rename(string? name)
    {
        // Mantengo la semántica original (permitías null). Si prefieres obligar a nombre no-nulo, lo cambiamos.
        Name = string.IsNullOrWhiteSpace(name) ? null : name.Trim();
    }

    public void SetObsolete(bool value) => IsObsolete = value;
}

