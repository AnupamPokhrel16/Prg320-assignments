using System;
using Week3Library.CustomException;
using Week3Library.Interface;
using Week3Library.Utilities;

namespace Week3Library.Model
{
    /// <summary>
    /// Derived class demonstrating INHERITANCE from LibraryItemBase.
    /// Adds Newspaper-specific Editor field and overrides DisplayInfo() for POLYMORPHISM.
    /// </summary>
    public class Newspaper : LibraryItemBase
    {
        // ENCAPSULATION: Private editor field with public property validation.
        private string _editor = string.Empty;

        // PROPERTY: Public interface with validation for editor field.
        public string Editor
        {
            get => _editor;
            set => _editor = ValidateEditor(value);
        }

        // Constructor: Calls base LibraryItemBase constructor, validates editor through property.
        public Newspaper(string title, string publisher, int publicationYear, string editor)
            : base(title, publisher, publicationYear)
        {
            Editor = editor;
        }

        // VALIDATION: Editor must be 5+ chars, auto-capitalizes. Newspaper-specific logic.
        private string ValidateEditor(string value)
        {
            value = value?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidItemDataException("Editor cannot be null or empty.");
            }
            if (value.Length < 5)
            {
                throw new InvalidItemDataException("Editor must be at least 5 characters long.");
            }
            if (!char.IsUpper(value[0]))
            {
                value = char.ToUpper(value[0]) + value[1..];
            }
            return value;
        }

        // POLYMORPHISM: Overrides base DisplayInfo() with Newspaper-specific format including Editor.
        public override void DisplayInfo()
        {
            Helper.TypeWrite($"[Newspaper] Title: {Title}, Publisher: {Publisher}, Year: {PublicationYear}, Editor: {Editor}");
        }
    }
}
