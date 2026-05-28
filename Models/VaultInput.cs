namespace CDB_Calculator;

public class VaultInput
{
    public decimal InitialAmount { get; set; }
    public ushort Months { get; set; }
    public string? VaultName { get; set; }
    public decimal MonthlyRate { get; set; } = 0.0085m;
}