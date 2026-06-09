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

            if (decimal.TryParse(ReadLine(), out principal) && principal >= 0) break;

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

            if (int.TryParse(ReadLine(), out months) && months >= 0) break;

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
    public int GetInteractiveOption()
    {
        while (true)
        {


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
