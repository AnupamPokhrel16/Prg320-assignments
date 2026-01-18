using System;

// Simple Banking System
public static class Banking
{
    // Entry point for the banking system. Presents a loop with options
    // handled via a switch statement
    public static void RunSimpleBankSystem()
    {
        const int correctPin = 0000; // demo PIN
        const int maxAttempts = 3;   // lockout threshold


        //if we ask for input pin by himself then
        // Console.Write("Set your PIN: ");
        // int userPin = int.Parse(Console.ReadLine());
        // Then we use userPin instead of correctPin in AuthenticateUser method call
        // if (!AuthenticateUser(userPin, maxAttempts))


        // Authenticate the user before allowing any operations.
        if (!AuthenticateUser(correctPin, maxAttempts))// if authentication fails
        {
            Console.ForegroundColor = ConsoleColor.Red; //color red for error
            Console.WriteLine("Access denied. Too many invalid PIN attempts.");
            Console.ResetColor();
            return;
        }

        bool exitBank = false; // Flag to control exit from banking system
        decimal balance = 0m; // initial balance is zero 

        while (!exitBank)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n******************************");
            Console.WriteLine("     Simple Banking System     ");
            Console.WriteLine("******************************");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Balance Inquiry");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            Console.ResetColor();

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    balance = Deposit(balance);
                    break;
                case "2":
                    balance = Withdraw(balance);
                    break;
                case "3":
                    ShowBalance(balance);
                    break;
                case "4":
                    exitBank = true;
                    Console.WriteLine("Exiting Banking System. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1-4.");
                    break;
            }
        }
    }

    // Validates PIN with up to maxAttempts tries.
    // Returns true when authentication succeeds.
    private static bool AuthenticateUser(int correctPin, int maxAttempts)
    {
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            Console.Write("Enter PIN: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int pin) && pin == correctPin)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Login successful.");
                Console.ResetColor();
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Invalid PIN. Attempt {attempt} of {maxAttempts}.");
            Console.ResetColor();
        }
        return false;
    }

    // Handles deposit operation. Ensures positive input.
    private static decimal Deposit(decimal balance)
    {
        Console.Write("Enter deposit amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        balance += amount;
        Console.WriteLine($"Deposited Nrs {amount}. New balance: Nrs {balance}");
        return balance;
    }

    // Handles withdrawal with validation to prevent overdraft.
    private static decimal Withdraw(decimal balance)
    {
        Console.Write("Enter withdrawal amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        if (amount > balance)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: Insufficient funds. Withdrawal denied.");
            Console.ResetColor();
            return balance;
        }
        balance -= amount;
        Console.WriteLine($"Withdrew Nrs {amount}. New balance: Nrs {balance}");
        return balance;
    }

    // Displays the current balance.
    private static void ShowBalance(decimal balance)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Current balance: Nrs {balance}");
        Console.ResetColor();
    }
}
