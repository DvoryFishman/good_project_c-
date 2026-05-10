using System;

namespace BL.BO
{
    public class BlNotFoundException : Exception
    {
        public BlNotFoundException() : base("Item not found") { }

        public BlNotFoundException(string message) : base(message) { }

        public BlNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}