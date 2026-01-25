using System;
using System.Collections.Generic;
using System.Linq;
using Week3Library.CustomException;
using Week3Library.Model;

namespace Week3Library.Service
{
    /// <summary>
    /// Service layer for library operations. Demonstrates:
    /// ENCAPSULATION: Private collection with controlled access via methods.
    /// POLYMORPHISM: Works with Item base type (Books and Magazines).
    /// ABSTRACTION: Hides collection management complexity.
    /// </summary>
    public class LibraryService
    {
        // ENCAPSULATION: Private collection stores both Books and Magazines as Item base type.
        private readonly List<Item> _items = new();

        // METHOD: Adds item to library. Checks duplicates before adding.
        // POLYMORPHISM: Works with any Item-derived type (Book, Magazine).
        public void AddItem(Item item)
        {
            if (IsDuplicate(item))
            {
                throw new DuplicateEntryException("An item with the same Title, Publisher, and Publication Year already exists.");
            }
            _items.Add(item);
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
                // Polymorphic call: Each item type displays its own format (Book/Magazine).
                item.DisplayItems();
                index++;
            }
        }

        // HELPER: Checks if item exists by Title, Publisher, Year (case-insensitive).
        // Returns true if duplicate found, false otherwise.
        private bool IsDuplicate(Item newItem)
        {
            return _items.Any(existing =>
                string.Equals(existing.Title, newItem.Title, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(existing.Publisher, newItem.Publisher, StringComparison.OrdinalIgnoreCase) &&
                existing.PublicationYear == newItem.PublicationYear);
        }
    }
}
