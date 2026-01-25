using System;
using Week3Library.CustomException;

namespace Week3Library.Model
{
    /// <summary>
    /// Derived class demonstrating INHERITANCE from Item.
    /// Adds Magazine-specific IssueNumber field and overrides DisplayItems() for POLYMORPHISM.
    /// </summary>
    public class Magazine : Item
    {
        // ENCAPSULATION: Private issue number field with public property validation.
        private int _issueNumber;

        // PROPERTY: Public interface with validation for issue number field.
        public int IssueNumber
        {
            get => _issueNumber;
            set => _issueNumber = ValidateIssueNumber(value);
        }

        // Constructor: Calls base Item constructor, validates issue number through property.
        public Magazine(string title, string publisher, int publicationYear, int issueNumber)
            : base(title, publisher, publicationYear)
        {
            IssueNumber = issueNumber;
        }

        // VALIDATION: Issue number must be positive integer. Magazine-specific logic.
        private int ValidateIssueNumber(int value)
        {
            if (value <= 0)
            {
                throw new InvalidItemDataException("Issue number must be a positive integer.");
            }
            return value;
        }

        // POLYMORPHISM: Overrides base DisplayItems() with Magazine-specific format including IssueNumber.
        public override void DisplayItems()
        {
            Console.WriteLine($"[Magazine] Title: {Title}, Publisher: {Publisher}, Year: {PublicationYear}, Issue: {IssueNumber}");
        }
    }
}
