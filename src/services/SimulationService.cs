namespace CdbCalculator;

public class SimulationService
{
    private readonly InvestmentCalculator _calculator;

    public SimulationService(InvestmentCalculator calculator)
    {
        _calculator = calculator;
    }

    public SimulationLog RunDirect(Investment vault, DynamicRates marketRate, decimal monthlyDeposit)
    {
        List<MonthlyEntry> entries = new();
        decimal balance = vault.Principal;

        for (int month = 1; month <= vault.Months; month++)
        {
            decimal rate = marketRate.Update();
            decimal deposit = month == 1 ? 0 : monthlyDeposit;

            decimal newBalance = _calculator.ApplyInterest(balance, rate, deposit);

            decimal profit = newBalance - balance - deposit;

            entries.Add(new MonthlyEntry
            {
                Month = month,
                Balance = newBalance,
                Profit = profit,
                Rate = rate
            });

            balance = newBalance;
        }

        return new SimulationLog
        {
            InvestmentName = vault.Name ?? "Geral",
            InitialPrincipal = vault.Principal,
            History = entries
        };
    }

    public SimulationLog RunInteractive(Investment vault, DynamicRates marketRate, Func<decimal> getMonthlyDeposit, Func<MonthlyEntry, int> getInteractiveOption)
    {
        List<MonthlyEntry> entries = new();
        decimal balance = vault.Principal;
        int month = 1;

        while (month <= vault.Months)
        {
            decimal rate = marketRate.Update();
            decimal monthlyDeposit = month == 1 ? 0 : getMonthlyDeposit();

            decimal newBalance = _calculator.ApplyInterest(balance, rate, monthlyDeposit);

            decimal profit = newBalance - balance - monthlyDeposit;

            var currentEntry = new MonthlyEntry
            {
                Month = month,
                Balance = newBalance,
                Profit = profit,
                Rate = rate
            };

            entries.Add(currentEntry);
            balance = newBalance;

            if (month == vault.Months) break;

            int mode = getInteractiveOption(currentEntry);

            month++;

            if (mode == 2 && month <= vault.Months)
            {
                entries.AddRange(CompleteSimulation(vault, marketRate, balance, month));
                break;
            }
            if (mode == 3)
            {
                break;
            }
        }

        return new SimulationLog
        {
            InvestmentName = vault.Name ?? "Geral",
            InitialPrincipal = vault.Principal,
            History = entries
        };
    }

    private List<MonthlyEntry> CompleteSimulation(Investment vault, DynamicRates marketRate, decimal balance, int lastMonth)
    {
        List<MonthlyEntry> entries = new();

        for (int month = lastMonth; month <= vault.Months; month++)
        {
            decimal rate = marketRate.Update();

            decimal newBalance = _calculator.ApplyInterest(balance, rate);

            decimal profit = newBalance - balance;

            entries.Add(new MonthlyEntry
            {
                Month = month,
                Balance = newBalance,
                Profit = profit,
                Rate = rate
            });

            balance = newBalance;
        }

        return entries;
    }
}
