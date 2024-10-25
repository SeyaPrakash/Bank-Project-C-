using System;

namespace Exceptions
{
    public class CustomerException : Exception
    {
        public CustomerException(string message) : base(message) { }
        public CustomerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
