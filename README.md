# CDB Investment Calculator

A console application built with C# and .NET 8 that simulates CDB (Certificado de Depósito Bancário) investment growth over time, featuring both direct and interactive simulation modes with a dynamic market interest rate model.

## Overview

This project simulates how a fixed-income investment grows month by month, including monthly contributions and a fluctuating interest rate that mimics real market behavior through a mean-reversion algorithm. It was built as a learning project to apply object-oriented design, separation of concerns, and clean architecture principles in a practical financial context.

## Features

- **Two simulation modes**
  - **Direct mode**: runs the full simulation automatically and displays a final report.
  - **Interactive mode**: lets the user decide, month by month, whether to continue, jump to the final result, or exit early.
- **Dynamic market rate simulation**: interest rates fluctuate within a defined range using a custom mean-reversion algorithm, instead of a static fixed rate.
- **Monthly contributions**: users can add a recurring deposit on top of the initial principal.
- **Detailed simulation report**: month-by-month breakdown of balance, profit, and applied rate.

## Architecture

The project follows a layered structure to keep business logic decoupled from the console interface:

```
CdbCalculator/src/
├── Program.cs              # Entry point and application flow
├── models/
│   ├── Investment.cs       # Represents the initial investment configuration
│   ├── MonthlyEntry.cs     # Represents a single month's result
│   └── SimulationLog.cs    # Represents the full simulation history
├── services/
│   ├── DynamicRates.cs         # Simulates fluctuating market interest rates
│   ├── InvestmentCalculator.cs # Applies interest and deposit calculations
│   └── SimulationService.cs    # Orchestrates the month-by-month simulation
└── ui/
    └── ConsoleView.cs       # Handles all console input/output
```

The `SimulationService` receives the console's input methods as delegates (`Func<T>`), which keeps the simulation logic independent from the console implementation — the service has no direct dependency on `Console`.

## Interest Rate Model

Instead of using a fixed interest rate, this project implements a **mean-reversion model**: the rate randomly drifts within a defined range (0.80% to 0.90% monthly), but the probability distribution is weighted to pull the rate back toward the center whenever it gets close to either boundary. This produces more realistic rate movement than pure randomness while still keeping the simulation unpredictable.

## How to Run

```bash
git clone https://github.com/kkcire/cdb-calculator.git
cd cdb-calculator/src
dotnet run
```

**Requirements:** .NET 8 SDK

## Example Output

```
==================================================
||            FINAL SIMULATION REPORT           ||
==================================================

Vault Name:        Emergency Fund
Initial Principal: R$1,000.00
Total Duration:    12 Month(s)
--------------------------------------------------
Month  | Rate (%)   | Profit       | Balance       
--------------------------------------------------
1      | 0.850%     | R$8.50       | R$1,008.50    
2      | 0.870%     | R$108.78     | R$1,317.28    
...
--------------------------------------------------
Final Accumulated Wealth: R$XX,XXX.XX
```

## What I Learned

This project was an exercise in applying object-oriented programming concepts beyond syntax — particularly encapsulation, separation of concerns, and decoupling business logic from I/O using delegates. It also involved deriving a mean-reversion formula for interest rate simulation from first principles, and debugging a subtle rounding bias caused by C#'s integer truncation behavior in `DynamicRates.Update()`.

## Roadmap

- [ ] Sum of the profits in the final report
- [ ] Display the month deposit that month in the final report
- [ ] Unit tests with xUnit for `InvestmentCalculator` and `DynamicRates`
- [ ] XML documentation comments
- [ ] Extract repeated console formatting logic into reusable helper methods

## Tech Stack

- C# 12
- .NET 8
- Console Application

---

Built by [Erick Magagna](https://github.com/kkcire) as part of a structured self-study path in back-end development.