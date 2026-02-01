using System;

namespace Week3Library.CustomException
{
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException(string message) : base(message)
        {
        }

        public DuplicateEntryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
