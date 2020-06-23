using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeMessaging
{
    /// <summary>
    /// Exception raised when trying to interact with chrome while the extension is not registered in the Windows Registry
    /// </summary>
    [Serializable]
    public class NotRegisteredWithChromeException : Exception
    {
        internal NotRegisteredWithChromeException() { }
        internal NotRegisteredWithChromeException(string message) : base(message) { }
        internal NotRegisteredWithChromeException(string message, Exception inner) : base(message, inner) { }
        internal NotRegisteredWithChromeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
