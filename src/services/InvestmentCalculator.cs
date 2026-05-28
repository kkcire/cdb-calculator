namespace CDB_Calculator;

public class InvestmentCalculator
{
    public decimal ApplyInterest(decimal balance, decimal rate, decimal monthlyApply = 0)
    {
        return (balance + monthlyApply) * (1 + rate);
    }
}