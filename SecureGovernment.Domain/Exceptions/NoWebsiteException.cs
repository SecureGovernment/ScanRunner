using System;

namespace SecureGovernment.Domain.Exceptions
{

    [Serializable]
    public class NoWebsiteException : Exception
    {
        public NoWebsiteException() : base("Expected a website but no website was provided!") { }
        public NoWebsiteException(string message) : base(message) { }
        public NoWebsiteException(string message, Exception inner) : base(message, inner) { }
        protected NoWebsiteException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
