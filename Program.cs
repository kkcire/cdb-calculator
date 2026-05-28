using CDB_Calculator;

var menu = new MenuHandler();

var vault = menu.CreateVaultData();

decimal monthlyApply = menu.AskMonthlyApply();

menu.RunSimulation(vault, monthlyApply);