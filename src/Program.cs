using CdbCalculator;

InvestmentCalculator calculator = new();
SimulationService service = new(calculator);
ConsoleView view = new();
DynamicRates marketRate = new();


Investment vault = view.CreateVaultData();
int mode = view.GetSimulationMode();

SimulationLog log;

// 3. Execute chosen simulation architecture via view delegates
if (mode == 1)
{
    log = service.RunInteractive(
        vault,
        marketRate,
        getMonthlyDeposit: view.GetMonthlyDeposit,
        getInteractiveOption: view.GetInteractiveOption,
        displayProgress: view.DisplayProgress
    );
}
else
{
    decimal constantDeposit = view.GetMonthlyDeposit();
    log = service.RunDirect(vault, marketRate, constantDeposit);
}

// 4. Final Execution Report Display
Clear();
ForegroundColor = ConsoleColor.DarkGreen;
WriteLine("=================================================");
WriteLine("||               FINAL SIMULATION REPORT        ||");
WriteLine("=================================================");
ResetColor();

WriteLine($"\nVault Name:        {log.InvestmentName}");
WriteLine($"Initial Principal: {log.InitialPrincipal:C}");
WriteLine($"Total Duration:    {log.History.Count} Month(s)");
WriteLine(new string('-', 50));

ForegroundColor = ConsoleColor.Gray;
WriteLine($"{"Month",-6} | {"Rate (%)",-10} | {"Profit", -12} | {"Balance",-14}");
WriteLine(new string('-', 50));
ResetColor();

foreach (var entry in log.History)
{
    WriteLine($"{entry.Month, -6} | {entry.Rate, -10:P3} | {entry.Profit, -12:C} | {entry.Balance, -14:C}");
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