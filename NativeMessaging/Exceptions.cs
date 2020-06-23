using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeMessaging
{
    [Serializable]
    public class NotRegisteredWithChromeException : Exception
    {
        public NotRegisteredWithChromeException() { }
        public NotRegisteredWithChromeException(string message) : base(message) { }
        public NotRegisteredWithChromeException(string message, Exception inner) : base(message, inner) { }
        protected NotRegisteredWithChromeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
