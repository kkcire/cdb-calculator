# CDB Investment Calculator

A console application built with C# and .NET 8 that simulates CDB (Certificado de Depósito Bancário) investment growth over time, featuring both direct and interactive simulation modes with a dynamic market interest rate model.

## Overview

This project simulates how a fixed-income investment grows month by month, including monthly deposits and a fluctuating interest rate through a custom mathematical algorithm. Users can run the simulation under two distinct modes:

* **Direct Mode:** An automated execution flow that processes the entire duration and displays the final report instantly.
* **Interactive Mode:** A step-by-step loop where the user controls the progression month by month, deciding the recurring deposit amount in real-time, with options to skip to the end or exit early.

It was built as a learning project to apply object-oriented design, strict separation of concerns, and clean architecture principles within a practical financial context.

## Architecture

The project follows a layered structure to keep business logic decoupled from the console interface:

```
src/
├── Program.cs                  # Entry point and application flow
├── models/
│   ├── Investment.cs           # Represents the initial investment configuration
│   ├── MonthlyEntry.cs         # Represents a single month's result
│   └── SimulationLog.cs        # Represents the full simulation history
├── services/
│   ├── DynamicRates.cs         # Simulates fluctuating market interest rates
│   ├── InvestmentCalculator.cs # Applies compound interest and deposit calculations
│   └── SimulationService.cs    # Orchestrates the month-by-month simulation
└── ui/
    └── ConsoleView.cs          # Handles all console input/output
```

---

### Dynamic Rate Algorithm

Instead of a static interest rate, `DynamicRates.cs` generates a new rate each month using a custom boundary-biased algorithm. The core idea: the closer the rate gets to either limit, the stronger the push back toward the center.

The adjustment range is calculated as follows:

<img src="docs/formula.jpeg" width="450"/>

Where `B_m` and `B_M` define the lower and upper bounds of a random integer draw. The resulting `trend` value is then scaled and added to the current rate, which is finally clamped within `[m, M]` to prevent boundary violations.

The further the rate drifts toward one boundary, the more the random range shifts in the opposite direction — creating a self-correcting fluctuation without any fixed pattern.

## How to Run

```bash
git clone https://github.com/kkcire/cdb-calculator.git
cd cdb-calculator/src
dotnet run
```

**Requirements:** .NET 8 SDK

## Example Output

```text
==================================================
||            FINAL SIMULATION REPORT           ||
==================================================

Vault Name:        Emergency Fund
Initial Principal: R$ 1.000,00
Total Duration:    12 Month(s)
-----------------------------------------------------------------
Month  | Rate (%)   | Profit       | Balance        | Month Deposit
-----------------------------------------------------------------
1      | 0,848%     | R$ 8,48      | R$ 1.008,48    | + R$ 0,00
2      | 0,838%     | R$ 10,13     | R$ 1.218,61    | + R$ 200,00
3      | 0,838%     | R$ 11,89     | R$ 1.430,49    | + R$ 200,00
...
12     | 0,843%     | R$ 28,59     | R$ 3.419,83    | + R$ 200,00
-----------------------------------------------------------------
Final Balance: R$ 3.419,83 | Total Profit: R$ 219,83
```

## What I Learned

* **Inversion of Control (IoC)** — `SimulationService` receives user input methods as `Func<T>` delegates, keeping business logic completely independent from the console layer. The service has no direct dependency on `Console`.
* **Object-Oriented Programming** — Applied encapsulation, abstraction, and aggregation throughout. Each class has a single, well-defined responsibility with no cross-layer leakage.
* **Immutability by Design** — Models (`Investment`, `MonthlyEntry`, `SimulationLog`) are implemented as `records` with `required` and `init` properties, ensuring data integrity after creation.
* **SOLID — Single Responsibility Principle** — Every class does exactly one thing: `InvestmentCalculator` calculates, `DynamicRates` generates rates, `ConsoleView` handles I/O, `SimulationService` orchestrates. None of them overlap.
* **Modern C# 12 Syntax** — Primary constructors, `record` types, target-typed `new()`, and `Math.Clamp()` used throughout.
* **Custom Algorithm Design** — The dynamic rate formula was derived from first principles on paper before implementation. No external library or reference — pure analytical reasoning to produce a self-correcting fluctuation algorithm.

## Roadmap

- [x] Sum of profits displayed in the final report
- [x] Monthly deposit visible per row in the final report
- [ ] Unit tests with xUnit for `InvestmentCalculator` and `DynamicRates`
- [ ] XML documentation comments
- [ ] Extract repeated console formatting logic into reusable helper methods

## Tech Stack

- C# 12
- .NET 8
- Console Application

---

Built by [Erick Magagna](https://github.com/kkcire) as part of a structured self-study path in back-end development.