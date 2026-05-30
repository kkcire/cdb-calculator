using CdbCalculator;

MenuHandler menu = new();
InvestmentCalculator investment = new();
DynamicRates rates = new();
Vault vault = menu.CreateVaultData();

decimal monthlyDeposit = menu.AskMonthlyApply();

menu.RunSimulation(vault, rates, monthlyDeposit);