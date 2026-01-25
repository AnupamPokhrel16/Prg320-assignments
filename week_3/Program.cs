using System;
using Week3Library.CustomException;
using Week3Library.Model;
using Week3Library.Service;

namespace Week3Library
{
    /// <summary>
    /// Main program demonstrating the Library Management System.
    /// Shows try-catch-finally exception handling and menu-driven user interface.
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            var libraryService = new LibraryService();
            bool exit = false;

            // MENU LOOP: Continuously presents options until user exits.
            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n==============================");
                Console.WriteLine("  Library Management System  ");
                Console.WriteLine("==============================");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Add Magazine");
                Console.WriteLine("3. Display All Items");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                Console.ResetColor();

                string choice = Console.ReadLine() ?? string.Empty;

                // TRY-CATCH-FINALLY: Handles exceptions gracefully with specific error handling.
                try
                {
                    switch (choice)
                    {
                        case "1":
                            var book = CreateBookFromInput();
                            libraryService.AddItem(book);
                            Console.WriteLine("Book added successfully.");
                            break;
                        case "2":
                            var magazine = CreateMagazineFromInput();
                            libraryService.AddItem(magazine);
                            Console.WriteLine("Magazine added successfully.");
                            break;
                        case "3":
                            libraryService.DisplayAllItems();
                            break;
                        case "4":
                            exit = true;
                            Console.WriteLine("Exiting system. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select 1-4.");
                            break;
                    }
                }
                catch (InvalidItemDataException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Input Error: {ex.Message}");
                    Console.ResetColor();
                }
                catch (DuplicateEntryException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Duplicate Error: {ex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                    Console.ResetColor();
                }
                finally
                {
                    Console.WriteLine("Returning to main menu...\n");
                }
            }
        }

        private static Book CreateBookFromInput()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter Publisher: ");
            string publisher = Console.ReadLine() ?? string.Empty;

            int year = ReadPublicationYear("Enter Publication Year (YYYY): ");

            Console.Write("Enter Author: ");
            string author = Console.ReadLine() ?? string.Empty;

            return new Book(title, publisher, year, author);
        }

        private static Magazine CreateMagazineFromInput()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter Publisher: ");
            string publisher = Console.ReadLine() ?? string.Empty;

            int year = ReadPublicationYear("Enter Publication Year (YYYY): ");

            int issueNumber = ReadInt("Enter Issue Number: ", mustBePositive: true);

            return new Magazine(title, publisher, year, issueNumber);
        }

        private static int ReadPublicationYear(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(input, out int year))
                {
                    if (year >= 1000 && year <= 9999)
                    {
                        return year;
                    }
                    Console.WriteLine("Year must be a valid four-digit year (1000-9999).");
                }
                else
                {
                    Console.WriteLine("Invalid number. Please try again.");
                }
            }
        }

        private static int ReadInt(string prompt, bool mustBePositive = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(input, out int value))
                {
                    if (!mustBePositive || value > 0)
                    {
                        return value;
                    }
                    Console.WriteLine("Value must be a positive integer.");
                }
                else
                {
                    Console.WriteLine("Invalid number. Please try again.");
                }
            }
        }
    }
}
