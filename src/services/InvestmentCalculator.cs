namespace CdbCalculator;

public class InvestmentCalculator
{
    public decimal ApplyInterest(decimal balance, decimal rate, decimal monthlyDeposit = 0)
    {
        return (balance + monthlyDeposit) * (1 + rate);
    }
}