using System;

namespace TreeGecko.Library.Common.Exceptions
{
    /// <summary>
    /// Thrown when a string does not meet the definition of a Guid.
    /// </summary>
    public class GuidFormatException : ApplicationException
    {
        public GuidFormatException()
        {
        }

        public GuidFormatException(string message)
            : base(message)
        {
        }
    }
}