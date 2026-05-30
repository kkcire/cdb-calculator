namespace CdbCalculator;

public record Vault
{
    public required decimal Principal { get; init; }
    public required int Months { get; init; }
    public string? Name { get; init; }
}