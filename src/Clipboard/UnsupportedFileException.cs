using System;
using System.Runtime.Serialization;

namespace Clipboard
{
    public class UnsupportedFileException : Exception
    {
        public UnsupportedFileException()
        {
        }

        public UnsupportedFileException(string message) : base(message)
        {
        }

        public UnsupportedFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
