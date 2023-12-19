using System;

namespace FastenUp.Runtime.Exceptions
{
    public class FastenUpException : Exception
    {
        /// <inheritdoc />
        public FastenUpException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public FastenUpException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}