# Banking.cs Explanation

This file implements a simple console banking system. All members are `static`, so you never create an object; you call methods directly on the `Banking` class.

## Structure
- `public static class Banking` — container for all banking functions; `static` means no instances are needed.
- `RunSimpleBankSystem()` — the main entry point that handles login and shows the banking menu loop.
- Helper methods (all `private static`):
  - `AuthenticateUser(int correctPin, int maxAttempts)` — checks PIN with limited tries.
  - `Deposit(decimal balance)` — reads an amount, adds to balance, returns new balance.
  - `Withdraw(decimal balance)` — reads an amount, prevents overdraft, returns new balance.
  - `ShowBalance(decimal balance)` — prints the current balance.

## Walkthrough of RunSimpleBankSystem
1) **Constants**
   - `const int correctPin = 0000;` demo PIN. Leading zeros are allowed in integer literals; value is zero here.
   - `const int maxAttempts = 3;` user gets 3 tries.

2) **Optional user-set PIN (commented)**
   - Shows how you could read a custom `userPin` and pass it to `AuthenticateUser` instead of `correctPin`.

3) **Authenticate**
   - Calls `AuthenticateUser(correctPin, maxAttempts)`; if it returns `false`, prints an error in red and exits.

4) **State for session**
   - `bool exitBank = false;` controls the main menu loop.
   - `decimal balance = 0m;` starting balance; `m` makes the literal a `decimal`, better for money than `double`.

5) **Menu loop**
   - Prints the banking menu with cyan text.
   - Reads `choice` via `Console.ReadLine()` and uses a `switch`:
     - `"1"` → `Deposit`, assigns the returned balance.
     - `"2"` → `Withdraw`, assigns the returned balance.
     - `"3"` → `ShowBalance`.
     - `"4"` → sets `exitBank = true` and prints goodbye.
     - `default` → prints "Invalid choice." message.

## Helper Details
### AuthenticateUser
- Loop `attempt` from 1 to `maxAttempts`.
- Prompts: `Enter PIN: `.
- Uses `int.TryParse(input, out int pin)` to safely parse.
- If parsed PIN equals `correctPin`, prints success in green and returns `true`.
- Otherwise prints attempt number in red and continues.
- After all attempts fail, returns `false`.

### Deposit
- Prompts: `Enter deposit amount: `.
- Parses with `decimal.Parse(Console.ReadLine())` (throws if input not numeric). Adds to `balance` and returns updated value.
- Prints confirmation: `Deposited Nrs {amount}. New balance: Nrs {balance}`.

### Withdraw
- Prompts: `Enter withdrawal amount: `.
- Parses with `decimal.Parse(...)`.
- If `amount > balance`, prints red error and leaves balance unchanged.
- Otherwise subtracts amount, prints `Withdrew Nrs {amount}. New balance: Nrs {balance}`.

### ShowBalance
- Prints `Current balance: Nrs {balance}` in blue.

## Notes and possible improvements
- `decimal.Parse` will throw if input is empty/non-numeric; `decimal.TryParse` would be safer.
- `correctPin` is `0000` (value 0). Change to a non-zero PIN if desired.
- No persistence: balance resets each run. To persist, write/read balance to a file.
