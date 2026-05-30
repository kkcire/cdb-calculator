namespace CdbCalculator;

public class MenuHandler
{
    public Vault CreateVaultData()
    {

        Clear();

        WriteLine("=== Create your Vault ===\n");

        WriteLine("What's the objective of yout Vault? ");
        string name = ReadLine() ?? "Geral";

        Clear();

        decimal principal;

        while (true)
        {
            WriteLine("How much do you want to apply inicially? ");
            if (decimal.TryParse(ReadLine(), out principal) && principal >= 0) break;

            WriteLine("[Error] Invalid Value. Please choose a valid number. \nPress any key to try again...");
            ReadKey();
            Clear();
        }

        Clear();

        int months;
        while (true)
        {
            WriteLine("For how many months do you want to invest? ");
            if (int.TryParse(ReadLine(), out months) && months >= 0) break;

            WriteLine("[Error] Invalid Value. Please choose a valid number. \nPress any key to try again...");
            ReadKey();
            Clear();
        }

        Clear();

        return new Vault
        {
            Principal = principal,
            Months = months,
            Name = name
        };
    }

    public decimal AskMonthlyApply()
    {
        decimal amount;

        while(true)
        {
            WriteLine("How much do you want to deposite every month?");
            string input = ReadLine() ?? "";

            Clear();

            if (decimal.TryParse(input, out amount) && amount >= 0) return amount;

            WriteLine("[Error] Invalid Value. Please choose a valid number. \nPress any key to continue...");
            ReadKey();
        }
    }

    public void RunSimulation(Vault vaultData, DynamicRates rate, decimal monthlyAmount)
    {
        decimal currentBalance = vaultData.Principal;
        decimal currentRate = rate.CurrentRate;

        currentRate = rate.UpdateRate();

        InvestmentCalculator investmentCalculator = new();

        for (ushort currentMonth = 1; currentMonth <= vaultData.Months; currentMonth++)
        {
            decimal monthlyApply = monthlyAmount;

            if (currentMonth == 1)
            {
                monthlyApply = 0;
            }

            currentBalance = investmentCalculator.ApplyInterest(currentBalance, currentRate, monthlyApply);
            WriteLine($"Month {currentMonth}: {currentBalance:C}");
        }
    }
}
