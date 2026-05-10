using System;

namespace BL.BO
{
    public class BlAlreadyExistsException : Exception
    {
        public BlAlreadyExistsException() : base("Item already exists") { }

        public BlAlreadyExistsException(string message) : base(message) { }

        public BlAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}