using System;

namespace Week3Library.CustomException
{
    public class InvalidItemDataException : Exception
    {
        public InvalidItemDataException(string message) : base(message)
        {
        }

        public InvalidItemDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
