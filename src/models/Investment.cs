namespace CdbCalculator;

public record Investment
{
    public required decimal Principal { get; init; }
    public required int Months { get; init; }
    public string? Name { get; init; }
}