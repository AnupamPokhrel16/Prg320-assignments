using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Week3Library.CustomException;
using Week3Library.Interface;
using Week3Library.Model;

namespace Week3Library.Service
{
    /// <summary>
    /// Service layer for library operations. Demonstrates:
    /// ENCAPSULATION: Private collection with controlled access via methods.
    /// POLYMORPHISM: Works with ILibraryItem interface type.
    /// ABSTRACTION: Hides collection and file management complexity.
    /// FILE I/O: Persists data to JSON file.
    /// </summary>
    public class LibraryService
    {
        // ENCAPSULATION: Private collection stores library items.
        private List<ILibraryItem> _items = new();
        private const string FileName = "libraryFile.json";

        // Constructor: Loads existing data on initialization.
        public LibraryService()
        {
            LoadData();
        }

        // METHOD: Adds item to library. Checks for duplicates and file presence before adding.
        // POLYMORPHISM: Works with any ILibraryItem-derived type (Book, Magazine, Newspaper).
        // FILE-AWARE: Detects duplicates against all items loaded from libraryFile.json
        public void AddItem(ILibraryItem item)
        {
            // Check for duplicates against the in-memory collection (loaded from file)
            if (CheckForDuplicates(item))
            {
                throw new DuplicateItemException("An item with the same Title, Publisher, and Publication Year already exists.");
            }

            _items.Add(item);
            SaveData(); // Save data to file after adding item
        }

        // METHOD: Displays all items. POLYMORPHISM: Each item displays its specific format.
        public void DisplayAllItems()
        {
            if (_items.Count == 0)
            {
                Console.WriteLine("No items in the library yet.");
                return;
            }

            Console.WriteLine($"Total items: {_items.Count}");
            int index = 1;
            foreach (var item in _items)
            {
                Console.Write($"{index}. ");
                // Polymorphic call: Each item type displays its own format (Book/Magazine/Newspaper).
                item.DisplayInfo();
                index++;
            }
        }

        // METHOD: Removes item from library by title.
        public void RemoveItem(string title)
        {
            var item = _items.FirstOrDefault(i =>
                string.Equals(i.Title, title, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                _items.Remove(item);
                SaveData();
                Console.WriteLine($"Item '{title}' removed successfully.");
            }
            else
            {
                Console.WriteLine($"Item '{title}' not found.");
            }
        }

        // METHOD: Searches for items by title.
        public void SearchByTitle(string title)
        {
            var results = _items.Where(i =>
                i.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            if (results.Count == 0)
            {
                Console.WriteLine($"No items found with title containing '{title}'.");
                return;
            }

            Console.WriteLine($"Found {results.Count} item(s):");
            int index = 1;
            foreach (var item in results)
            {
                Console.Write($"{index}. ");
                item.DisplayInfo();
                index++;
            }
        }

        // METHOD: Searches for items by author (Books only).
        public void SearchByAuthor(string author)
        {
            var results = _items.OfType<Book>()
                .Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine($"No books found by author '{author}'.");
                return;
            }

            Console.WriteLine($"Found {results.Count} book(s):");
            int index = 1;
            foreach (var book in results)
            {
                Console.Write($"{index}. ");
                book.DisplayInfo();
                index++;
            }
        }

        // METHOD: Sorts items by title.
        public void SortByTitle()
        {
            _items = _items.OrderBy(i => i.Title).ToList();
            Console.WriteLine("Items sorted by title.");
        }

        // METHOD: Sorts items by year.
        public void SortByYear()
        {
            _items = _items.OrderBy(i => i.PublicationYear).ToList();
            Console.WriteLine("Items sorted by publication year.");
        }

        // HELPER: Checks if item already exists in the library (file-aware).
        // Iterates through all items loaded from file and compares each with new item.
        private bool CheckForDuplicates(ILibraryItem newItem)
        {
            foreach (var existingItem in _items)
            {
                if (IsDuplicate(existingItem, newItem))
                {
                    return true;
                }
            }
            return false;
        }

        // HELPER: Comprehensive duplicate detection with type-safe comparison.
        // Step 1: Compares item types (Book vs Magazine vs Newspaper)
        // Step 2: Compares shared properties (Title, Publisher, PublicationYear)
        // Step 3: Compares item-specific properties (Author, IssueNumber, Editor)
        private bool IsDuplicate(ILibraryItem existingItem, ILibraryItem newItem)
        {
            // STEP 1: Check if both items are of the same type.
            // Items of different types cannot be duplicates.
            if (!AreItemTypesMatching(existingItem, newItem))
            {
                return false;
            }

            // STEP 2: Check common properties shared by all item types.
            if (!AreSharedPropertiesMatching(existingItem, newItem))
            {
                return false;
            }

            // STEP 3: Check item-specific properties based on type.
            return AreItemSpecificPropertiesMatching(existingItem, newItem);
        }

        // HELPER: Compares item types to ensure they are the same class.
        private bool AreItemTypesMatching(ILibraryItem existingItem, ILibraryItem newItem)
        {
            return existingItem.GetType() == newItem.GetType();
        }

        // HELPER: Compares common properties shared by all library items.
        // These properties exist on every item type (Book, Magazine, Newspaper).
        private bool AreSharedPropertiesMatching(ILibraryItem existingItem, ILibraryItem newItem)
        {
            bool titleMatches = string.Equals(
                existingItem.Title,
                newItem.Title,
                StringComparison.OrdinalIgnoreCase);

            bool publisherMatches = string.Equals(
                existingItem.Publisher,
                newItem.Publisher,
                StringComparison.OrdinalIgnoreCase);

            bool yearMatches =
                existingItem.PublicationYear == newItem.PublicationYear;

            return titleMatches && publisherMatches && yearMatches;
        }

        // HELPER: Compares item-specific properties unique to each item type.
        // Uses pattern matching to handle different types appropriately.
        private bool AreItemSpecificPropertiesMatching(ILibraryItem existingItem, ILibraryItem newItem)
        {
            return (existingItem, newItem) switch
            {
                // For Books: Compare authors (case-insensitive)
                (Book existingBook, Book newBook) =>
                    string.Equals(
                        existingBook.Author,
                        newBook.Author,
                        StringComparison.OrdinalIgnoreCase),

                // For Magazines: Compare issue numbers (exact match)
                (Magazine existingMag, Magazine newMag) =>
                    existingMag.IssueNumber == newMag.IssueNumber,

                // For Newspapers: Compare editors (case-insensitive)
                (Newspaper existingNews, Newspaper newNews) =>
                    string.Equals(
                        existingNews.Editor,
                        newNews.Editor,
                        StringComparison.OrdinalIgnoreCase),

                // Unknown type combination - not a duplicate
                _ => false
            };
        }

        // FILE I/O: Saves library data to a text file.
        private void SaveData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FileName))
                {
                    foreach (var item in _items)
                    {
                        writer.WriteLine(SerializeItem(item));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving library data: {ex.Message}");
            }
        }

        // FILE I/O: Loads library data from text file.
        private void LoadData()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    _items.Clear();
                    using (StreamReader reader = new StreamReader(FileName))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var item = DeserializeItem(line);
                            if (item != null)
                            {
                                _items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading library data: {ex.Message}");
            }
        }

        // Helper method to serialize an item to string format.
        private string SerializeItem(ILibraryItem item)
        {
            return item switch
            {
                Book book => $"BOOK|{book.Title}|{book.Publisher}|{book.PublicationYear}|{book.Author}",
                Magazine mag => $"MAGAZINE|{mag.Title}|{mag.Publisher}|{mag.PublicationYear}|{mag.IssueNumber}",
                Newspaper news => $"NEWSPAPER|{news.Title}|{news.Publisher}|{news.PublicationYear}|{news.Editor}",
                _ => string.Empty
            };
        }

        // Helper method to deserialize an item from string format.
        private ILibraryItem? DeserializeItem(string line)
        {
            var parts = line.Split('|');
            if (parts.Length < 5)
                return null;

            try
            {
                return parts[0] switch
                {
                    "BOOK" => new Book(parts[1], parts[2], int.Parse(parts[3]), parts[4]),
                    "MAGAZINE" => new Magazine(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4])),
                    "NEWSPAPER" => new Newspaper(parts[1], parts[2], int.Parse(parts[3]), parts[4]),
                    _ => null
                };
            }
            catch
            {
                return null;
            }
        }
    }
}