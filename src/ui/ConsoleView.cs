namespace CdbCalculator;

public class ConsoleView
{
    public void HeaderFormat(string title)
    {
        ForegroundColor = ConsoleColor.DarkGreen;
        WriteLine($" --- {title} ---");
        ResetColor();
        WriteLine();
    }

    public Investment CreateVaultData()
    {
        ResetColor();

        Clear();
        ForegroundColor = ConsoleColor.DarkGreen;
        WriteLine("=================================================");
        WriteLine("||            CREATING A NEW VAULT             ||");
        WriteLine("=================================================");
        ResetColor();
        WriteLine();

        HeaderFormat("STEP 1: NAME YOUR VAULT");

        ForegroundColor = ConsoleColor.White;
        WriteLine(" > What's will be the name of your Vault? (ex: Emergency, Buy a PC, Invest)");
        string? name = ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            WriteLine("Defining the name as 'General'.");
            name = "General";

            ForegroundColor = ConsoleColor.DarkGray;
            WriteLine("Press any key to continue...");
            ResetColor();
            ReadKey();
        }

        Clear();

        decimal principal;

        while (true)
        {
            HeaderFormat("STEP 2: INITIAL DEPOSIT");

            WriteLine("> How much do you want to deposit initially? ");

            if (decimal.TryParse(ReadLine(), out principal) && principal > 0) break;

            Clear();
            ForegroundColor = ConsoleColor.Red;
            WriteLine("[ERROR] Invalid Value.");
            WriteLine("Please enter a positive numeric value.");
            ForegroundColor = ConsoleColor.DarkGray;
            WriteLine("\nPress any key to try again...");
            ResetColor();
            ReadKey();
            Clear();
        }

        Clear();

        int months;
        while (true)
        {
            HeaderFormat("STEP 3: INVESTMENT PERIOD");

            WriteLine("> For how many months do you want to invest? ");

            if (int.TryParse(ReadLine(), out months) && months > 0) break;

            Clear();
            ForegroundColor = ConsoleColor.Red;
            WriteLine("[ERROR] Invalid Value.");
            WriteLine("Please enter a positive numeric value.");
            ForegroundColor = ConsoleColor.DarkGray;
            WriteLine("Press any key to try again...");
            ResetColor();
            ReadKey();
            Clear();
        }

        Clear();

        ForegroundColor = ConsoleColor.Green;
        WriteLine("> Vault configured successfully!");
        ResetColor();
        Thread.Sleep(1500);
        Clear();

        return new Investment
        {
            Principal = principal,
            Months = months,
            Name = name
        };
    }

    public decimal GetMonthlyDeposit()
    {
        decimal amount;

        while (true)
        {
            WriteLine("> How much do you want to deposit every month?");
            ResetColor();
            string input = ReadLine() ?? "";

            Clear();

            if (decimal.TryParse(input, out amount) && amount >= 0) return amount;

            ForegroundColor = ConsoleColor.Red;
            WriteLine("[ERROR] Invalid Value.");
            WriteLine("Please choose a valid number.");
            ForegroundColor = ConsoleColor.DarkGray;
            WriteLine("\nPress any key to try again...");
            ResetColor();
            ReadKey();
            Clear();
        }
    }

    public int GetSimulationMode()
    {
        while (true)
        {
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine("> Choose the investment simulation mode:\n");
            ResetColor();
            WriteLine("1. Interactive");
            WriteLine("2. Direct (Final Result)");


            if (int.TryParse(ReadLine(), out int option) && (option == 1 || option == 2))
            {
                Clear();
                return option;
            }

            Clear();

            ForegroundColor = ConsoleColor.Red;
            WriteLine("[Error] Invalid Option.");
            WriteLine("Please choose 1 or 2.");
            ForegroundColor = ConsoleColor.DarkGray;
            WriteLine("Press any key to try again...");
            ReadKey();
            ResetColor();
            Clear();
        }
    }
    public int GetInteractiveOption(MonthlyEntry entry)
    {
        while (true)
        {
            DisplayProgress(entry);

            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine("> How do you want to proceed?\n");
            ResetColor();

            WriteLine("1. Continue");

            WriteLine("2. Final Result");

            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine("3. Exit");
            ResetColor();


            if (int.TryParse(ReadLine(), out int option) && option >= 1 && option <= 3)
            {
                Clear();
                return option;
            }

            Clear();


            ForegroundColor = ConsoleColor.Red;
            WriteLine("[ERROR] Invalid Value.");
            WriteLine("Please choose a valid option.");
            ForegroundColor = ConsoleColor.DarkGray;
            WriteLine("\nPress any key to try again...");
            ResetColor();
            ReadKey();
            Clear();
        }
    }

    public void DisplayFinalReport(SimulationLog log)
    {
        Clear();
        ForegroundColor = ConsoleColor.DarkGreen;
        WriteLine("==================================================");
        WriteLine("||            FINAL SIMULATION REPORT           ||");
        WriteLine("==================================================");
        ResetColor();

        WriteLine($"\nVault Name:        {log.InvestmentName}");
        WriteLine($"Initial Principal: {log.InitialPrincipal:C}");
        WriteLine($"Total Duration:    {log.History.Count} Month(s)");
        WriteLine(new string('-', 50));

        ForegroundColor = ConsoleColor.Gray;
        WriteLine($"{"Month",-6} | {"Rate (%)",-10} | {"Profit",-12} | {"Balance",-14}");
        WriteLine(new string('-', 50));
        ResetColor();

        foreach (var entry in log.History)
        {
            WriteLine($"{entry.Month,-6} | {entry.Rate,-10:P3} | {entry.Profit,-12:C} | {entry.Balance,-14:C}");
        }

        WriteLine(new string('-', 50));

        if (log.History.Count > 0)
        {
            decimal finalBalance = log.History[^1].Balance;

            ForegroundColor = ConsoleColor.Green;
            WriteLine($"Final Accumulated Wealth: {finalBalance:C}");
            ResetColor();
        }

        ForegroundColor = ConsoleColor.DarkGray;
        WriteLine("\nPress any key to close the simulator...");
        ResetColor();
        ReadKey();
        Clear();
    }

    public void DisplayProgress(MonthlyEntry entry)
    {
        ForegroundColor = ConsoleColor.DarkGreen;
        WriteLine($"--- MONTH {entry.Month:00} PROGRESSION ---");
        ResetColor();
        WriteLine($" - Applied Market Rate:  {entry.Rate:P3}");
        WriteLine($" - Net Month Profit:     {entry.Profit:C}");
        WriteLine($" - Account Balance:      {entry.Balance:C}");
        ResetColor();
        WriteLine(new string('-', 35));
    }

}
