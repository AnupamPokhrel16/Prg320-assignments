using System;

namespace Week3Library.CustomException
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message)
        {
        }

        public DuplicateItemException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
