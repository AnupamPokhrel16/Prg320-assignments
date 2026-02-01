using System;

namespace Week3Library.Interface
{
    /// <summary>
    /// Interface defining the contract for all library items.
    /// Ensures consistent structure and behavior across different item types (Book, Magazine, Newspaper).
    /// </summary>
    public interface ILibraryItem
    {
        // Common properties for all library items
        string Title { get; set; }
        string Publisher { get; set; }
        int PublicationYear { get; set; }

        // Method to display item information in a formatted manner
        void DisplayInfo();
    }
}
