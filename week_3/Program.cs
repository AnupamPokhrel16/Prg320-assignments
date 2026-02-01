using System;
using Week3Library.CustomException;
using Week3Library.Model;
using Week3Library.Service;

namespace Week3Library
{
    /// Main program demonstrating the Library Management System.
    /// Shows try-catch-finally exception handling and menu-driven user interface.

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
                Console.WriteLine("3. Add Newspaper");
                Console.WriteLine("4. Display All Items");
                Console.WriteLine("5. Remove Item");
                Console.WriteLine("6. Search by Title");
                Console.WriteLine("7. Search by Author");
                Console.WriteLine("8. Sort by Title");
                Console.WriteLine("9. Sort by Year");
                Console.WriteLine("10. Exit");
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
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Book added successfully.");
                            Console.ResetColor();
                            break;
                        case "2":
                            var magazine = CreateMagazineFromInput();
                            libraryService.AddItem(magazine);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Magazine added successfully.");
                            Console.ResetColor();
                            break;
                        case "3":
                            var newspaper = CreateNewspaperFromInput();
                            libraryService.AddItem(newspaper);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Newspaper added successfully.");
                            Console.ResetColor();
                            break;
                        case "4":
                            libraryService.DisplayAllItems();
                            break;
                        case "5":
                            Console.Write("Enter the title of the item to remove: ");
                            string titleToRemove = Console.ReadLine() ?? string.Empty;
                            libraryService.RemoveItem(titleToRemove);
                            break;
                        case "6":
                            Console.Write("Enter the title to search for: ");
                            string titleToSearch = Console.ReadLine() ?? string.Empty;
                            libraryService.SearchByTitle(titleToSearch);
                            break;
                        case "7":
                            Console.Write("Enter the author to search for: ");
                            string authorToSearch = Console.ReadLine() ?? string.Empty;
                            libraryService.SearchByAuthor(authorToSearch);
                            break;
                        case "8":
                            libraryService.SortByTitle();
                            break;
                        case "9":
                            libraryService.SortByYear();
                            break;
                        case "10":
                            exit = true;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Exiting system. Goodbye!");
                            Console.ResetColor();
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select 1-10.");
                            break;
                    }
                }
                catch (InvalidItemDataException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Input Error: {ex.Message}");
                    Console.ResetColor();
                }
                catch (DuplicateItemException ex)
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

        private static Newspaper CreateNewspaperFromInput()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter Publisher: ");
            string publisher = Console.ReadLine() ?? string.Empty;

            int year = ReadPublicationYear("Enter Publication Year (YYYY): ");

            Console.Write("Enter Editor: ");
            string editor = Console.ReadLine() ?? string.Empty;

            return new Newspaper(title, publisher, year, editor);
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
