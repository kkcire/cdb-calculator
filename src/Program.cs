using CdbCalculator;

InvestmentCalculator calculator = new();
SimulationService service = new(calculator);
ConsoleView view = new();
DynamicRates marketRate = new();


Investment vault = view.CreateVaultData();
int mode = view.GetSimulationMode();

SimulationLog log;

if (mode == 1)
{
    log = service.RunInteractive(
        vault,
        marketRate,
        getMonthlyDeposit: view.GetMonthlyDeposit,
        getInteractiveOption: view.GetInteractiveOption
    );
}
else
{
    decimal constantDeposit = view.GetMonthlyDeposit();
    log = service.RunDirect(vault, marketRate, constantDeposit);
}

view.DisplayFinalReport(log);