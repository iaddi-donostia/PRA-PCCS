namespace PRA.PCCS.Domain.Shared;

public sealed class StructuredData
{
    public string Json { get; private set; } = "{}";

    private StructuredData() { } // EF
    public StructuredData(string json)
    {
        Json = string.IsNullOrWhiteSpace(json) ? "{}" : json.Trim();
    }

    public static StructuredData Empty => new("{}");
}

