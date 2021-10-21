namespace NativeMessaging
{
    /// <summary>
    /// Exception raised when trying to interact with chrome while the extension is not registered in the Windows Registry
    /// </summary>
    [Serializable]
    public class NotRegisteredWithBrowserException : Exception
    {
        internal NotRegisteredWithBrowserException() { }
        internal NotRegisteredWithBrowserException(string message) : base(message) { }
        internal NotRegisteredWithBrowserException(string message, Exception inner) : base(message, inner) { }
        internal NotRegisteredWithBrowserException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
