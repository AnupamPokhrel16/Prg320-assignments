# ConsoleProjects

This solution hosts multiple console demos:

- Task Management System
- Student Grading System
- Simple Banking System (added per assignment)

## Run the project

From the repository root, run:

```sh
dotnet run --project "ConsoleProjects/ConsoleProjects.csproj"
```

Select the desired demo from the main menu.

## Simple Banking System

Features:
- PIN login with 3 attempts
- Deposit, Withdraw, Balance Inquiry
- Validation for positive amounts and overdraft protection

### Quick usage
1. Choose `3. Simple Banking System` in the main menu.
2. Enter PIN `1234`.
3. Use options 1-4 to operate.

## Notes
- Monetary values use `decimal` to avoid floating-point rounding.
- Input validation loops ensure positive numbers for transactions.
- The banking logic lives in `Program.cs` to satisfy the assignment requirement.
