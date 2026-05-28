namespace CDB_Calculator;

public class MenuHandler
{
    public VaultInput CreateVaultData()
    {
        VaultInput vaultData = new();

        Clear();

        WriteLine("=== Create your Vault ===\n");

        WriteLine("What's the objective of yout Vault? ");
        vaultData.VaultName = ReadLine() ?? "Geral";

        Clear();

        decimal initialAmount;
        while (true)
        {
            WriteLine("How much do you want to apply inicially? ");
            if (decimal.TryParse(ReadLine(), out initialAmount) && initialAmount >= 0) break;

            WriteLine("[Error] Invalid Value. Please choose a valid number. \nPress any key to try again...");
            ReadKey();
            Clear();
        }
        vaultData.InitialAmount = initialAmount;

        Clear();

        ushort months;
        while (true)
        {
            WriteLine("For how many months do you want to invest? ");
            if (ushort.TryParse(ReadLine(), out months) && months >= 0) break;

            WriteLine("[Error] Invalid Value. Please choose a valid number. \nPress any key to try again...");
            ReadKey();
            Clear();
        }
        vaultData.Months = months;

        Clear();

        return vaultData;
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

    public void RunSimulation(VaultInput vaultData, decimal monthlyAmount)
    {
        decimal currentBalance = vaultData.InitialAmount;

        InvestmentCalculator investmentCalculator = new();

        for (ushort currentMonth = 1; currentMonth <= vaultData.Months; currentMonth++)
        {
            decimal monthlyApply = monthlyAmount;

            if (currentMonth == 1)
            {
                monthlyApply = 0;
            }

            currentBalance = investmentCalculator.ApplyInterest(currentBalance, vaultData.MonthlyRate, monthlyApply);
            WriteLine($"Month {currentMonth}: {currentBalance:C}");
        }
    }
}
