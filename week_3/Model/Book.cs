using System;
using Week3Library.CustomException;
using Week3Library.Interface;
using Week3Library.Utilities;

namespace Week3Library.Model
{
    /// <summary>
    /// Derived class demonstrating INHERITANCE from LibraryItemBase.
    /// Adds Book-specific Author field and overrides DisplayInfo() for POLYMORPHISM.
    /// </summary>
    public class Book : LibraryItemBase
    {
        // ENCAPSULATION: Private author field with public property validation.
        private string _author = string.Empty;

        // PROPERTY: Public interface with validation for author field.
        public string Author
        {
            get => _author;
            set => _author = ValidateAuthor(value);
        }

        // Constructor: Calls base Item constructor, validates author through property.
        public Book(string title, string publisher, int publicationYear, string author)
            : base(title, publisher, publicationYear)
        {
            Author = author;
        }

        // VALIDATION: Author must be 5+ chars, auto-capitalizes. Book-specific logic.
        private string ValidateAuthor(string value)
        {
            value = value?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidItemDataException("Author cannot be null or empty.");
            }
            if (value.Length < 5)
            {
                throw new InvalidItemDataException("Author must be at least 5 characters long.");
            }
            if (!char.IsUpper(value[0]))
            {
                value = char.ToUpper(value[0]) + value[1..];
            }
            return value;
        }

        // POLYMORPHISM: Overrides base DisplayInfo() with Book-specific format including Author.
        public override void DisplayInfo()
        {
            Helper.TypeWrite($"[Book] Title: {Title}, Author: {Author}, Publisher: {Publisher}, Year: {PublicationYear}");
        }
    }
}
