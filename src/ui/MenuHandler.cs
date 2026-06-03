namespace CdbCalculator;

public class ConsoleView
{
    public Investment CreateVaultData()
    {

        Clear();

        WriteLine("=== Create your Vault ===\n");

        WriteLine("What's the objective of your Vault? ");
        string name = ReadLine() ?? "Geral";

        Clear();

        decimal principal;

        while (true)
        {
            WriteLine("How much do you want to deposit initially? ");
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

        return new Investment
        {
            Principal = principal,
            Months = months,
            Name = name
        };
    }

    public decimal AskMonthlyApply()
    {
        decimal amount;

        while (true)
        {
            WriteLine("How much do you want to deposit every month?");
            string input = ReadLine() ?? "";

            Clear();

            if (decimal.TryParse(input, out amount) && amount >= 0) return amount;

            WriteLine("[Error] Invalid Value. Please choose a valid number. \nPress any key to continue...");
            ReadKey();
        }
    }

    public void RunDirectSimulation(Investment vault, DynamicRates monthlyRate)
    {
        decimal currentBalance = vault.Principal;
        decimal monthlyDepositBalance = this.AskMonthlyApply();

        InvestmentCalculator investmentCalculator = new();

        for (int currentMonth = 1; currentMonth <= vault.Months; currentMonth++)
        {
            decimal currentRate = monthlyRate.Update();
            decimal monthlyDeposit = monthlyDepositBalance;

            if (currentMonth == 1)
            {
                monthlyDeposit = 0;
            }

            currentBalance = investmentCalculator.ApplyInterest(currentBalance, currentRate, monthlyDeposit);
            WriteLine($"Month {currentMonth}: {currentBalance:C}");

        }
    }

    public void RunInteractiveSimulation(Investment vault, DynamicRates rate)
    {
        InvestmentCalculator investmentCalculator = new();

        decimal currentBalance = vault.Principal;
        int currentMonth = 1;

        while (currentMonth < vault.Months)
        {
            decimal monthlyDeposit = 0;

            if (currentMonth != 1) monthlyDeposit = this.AskMonthlyApply();
            decimal monthlyRate = rate.Update();

            currentBalance = investmentCalculator.ApplyInterest(currentBalance, monthlyRate, monthlyDeposit);
            WriteLine($"Month {currentMonth}: {currentBalance:C}\n");

            currentMonth++;

            int option = GetInteractiveOption();

            if (option == 1) continue;
            if (option == 2)
            {
                CompleteSimulation(vault, rate, currentBalance, currentMonth);
                break;
            }
            if (option == 3) break;


        }
    }

    public void CompleteSimulation(Investment vault, DynamicRates rate, decimal currentBalance, int currentMonth)
    {
        InvestmentCalculator investmentCalculator = new();

        while (currentMonth <= vault.Months)
        {
            decimal currentRate = rate.Update();

            currentBalance = investmentCalculator.ApplyInterest(currentBalance, currentRate);
            WriteLine($"Month {currentMonth}: {currentBalance:C}");


            currentMonth++;
        }
    }

    public int GetInteractiveOption()
    {
        while (true)
        {
            WriteLine("Choose an option");
            WriteLine("1. Continue");
            WriteLine("2. Final Result");
            WriteLine("3. Exit");

            if (int.TryParse(ReadLine(), out int option) && option >= 1 && option <= 3)
            {
                Clear();
                return option;
            }

            WriteLine("[Error] Invalid Option. Please choose a valid option. \nPress any key to try again...");
            ReadKey();
            Clear();
        }
    }

    public int GetSimulationMode()
    {
        while (true)
        {
            WriteLine("Choose the simulation mode");
            WriteLine("1. Interactive");
            WriteLine("2. Direct (Final Result)");

            if (int.TryParse(ReadLine(), out int option) && (option == 1 || option == 2))
            {
                Clear();
                return option;
            }

            WriteLine("[Error] Invalid Option. Please choose 1 or 2. \nPress any key to try again...");
            ReadKey();
            Clear();
        }
    }
}
