using System;
using Week3Library.CustomException;
using Week3Library.Interface;
using Week3Library.Utilities;

namespace Week3Library.Model
{
    /// <summary>
    /// Derived class demonstrating INHERITANCE from LibraryItemBase.
    /// Adds Magazine-specific IssueNumber field and overrides DisplayInfo() for POLYMORPHISM.
    /// </summary>
    public class Magazine : LibraryItemBase
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

        // POLYMORPHISM: Overrides base DisplayInfo() with Magazine-specific format including IssueNumber.
        public override void DisplayInfo()
        {
            Helper.TypeWrite($"[Magazine] Title: {Title}, Publisher: {Publisher}, Year: {PublicationYear}, Issue: {IssueNumber}");
        }
    }
}
