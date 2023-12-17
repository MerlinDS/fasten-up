using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace FastenUp.Runtime.Exceptions
{
    public class FastenUpException : Exception
    {

        /// <inheritdoc />
        protected FastenUpException([NotNull] SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

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