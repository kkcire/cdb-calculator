namespace CdbCalculator;

public record MonthlyEntry
{
    public required int Month { get; init; }
    public required decimal Balance { get; init; }
    public required decimal Profit { get; init; }
    public required decimal Rate { get; init; }

    public required decimal Deposit { get; init; }
}
