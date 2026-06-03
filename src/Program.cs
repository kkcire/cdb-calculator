using CdbCalculator;

ConsoleView menu = new();
DynamicRates rate = new();
Investment vault = menu.CreateVaultData();

int simulationMode = menu.GetSimulationMode();

if (simulationMode == 1)
{
    menu.RunInteractiveSimulation(vault, rate);
}
else
{
    menu.RunDirectSimulation(vault, rate);
}