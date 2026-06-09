namespace CdbCalculator;

public class SimulationLog
{
    public required string InvestmentName { get; init; }
    public required decimal InitialPrincipal { get; init; }
    public required List<MonthlyEntry> History { get; init; }
}
