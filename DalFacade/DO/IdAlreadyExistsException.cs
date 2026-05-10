using System;

namespace DO
{
    public class IdAlreadyExistsException : Exception
    {
        public IdAlreadyExistsException() : base("ID already exists") { }

        public IdAlreadyExistsException(string message) : base(message) { }

        public IdAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}