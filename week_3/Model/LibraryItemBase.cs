using System;
using Week3Library.CustomException;
using Week3Library.Interface;

namespace Week3Library.Model
{
    /// <summary>
    /// Abstract base class implementing ILibraryItem.
    /// Demonstrates ABSTRACTION, ENCAPSULATION, and INHERITANCE.
    /// Provides common structure and validation for all library items.
    /// </summary>
    public abstract class LibraryItemBase : ILibraryItem
    {
        // ENCAPSULATION: Private fields modified only through validated property setters.
        private string _title = string.Empty;
        private string _publisher = string.Empty;
        private int _publicationYear;

        // PROPERTIES: Public interface to private fields with validation.
        public string Title
        {
            get => _title;
            set => _title = ValidateTitle(value);
        }

        public string Publisher
        {
            get => _publisher;
            set => _publisher = ValidatePublisher(value);
        }

        public int PublicationYear
        {
            get => _publicationYear;
            set => _publicationYear = ValidatePublicationYear(value);
        }

        // Constructor: Protected to ensure validation during initialization.
        protected LibraryItemBase(string title, string publisher, int publicationYear)
        {
            Title = title;
            Publisher = publisher;
            PublicationYear = publicationYear;
        }

        // VALIDATION: Title must be 5+ chars, auto-capitalizes.
        protected virtual string ValidateTitle(string value)
        {
            value = value?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidItemDataException("Title cannot be null or empty.");
            }
            if (value.Length < 5)
            {
                throw new InvalidItemDataException("Title must be at least 5 characters long.");
            }
            if (!char.IsUpper(value[0]))
            {
                value = char.ToUpper(value[0]) + value[1..];
            }
            return value;
        }

        // VALIDATION: Publisher must be 6+ chars, auto-capitalizes.
        protected virtual string ValidatePublisher(string value)
        {
            value = value?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidItemDataException("Publisher cannot be null or empty.");
            }
            if (value.Length < 6)
            {
                throw new InvalidItemDataException("Publisher must be at least 6 characters long.");
            }
            if (!char.IsUpper(value[0]))
            {
                value = char.ToUpper(value[0]) + value[1..];
            }
            return value;
        }

        // VALIDATION: Year must be 4-digit (1000-9999).
        protected virtual int ValidatePublicationYear(int year)
        {
            if (year < 1000 || year > 9999)
            {
                throw new InvalidItemDataException("Publication year must be a valid four-digit year.");
            }
            return year;
        }

        // POLYMORPHISM: Abstract method for polymorphic display behavior across derived types.
        public abstract void DisplayInfo();
    }
}
