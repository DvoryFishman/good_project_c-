using System;

namespace DO
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException() : base("ID not found") { }

        public IdNotFoundException(string message) : base(message) { }

        public IdNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}