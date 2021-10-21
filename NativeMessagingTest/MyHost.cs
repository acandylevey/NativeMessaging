using NativeMessaging;
using Newtonsoft.Json.Linq;

namespace NativeMessagingTest
{
    public class MyHost : Host
    {
        private const bool SendConfirmationReceipt = true;

        public override string Hostname
        {
            get { return "com.google.chrome.example.echo"; }
        }

        public MyHost() : base(SendConfirmationReceipt)
        {

        }

        protected override void ProcessReceivedMessage(JObject data)
        {
            SendMessage(data);
        }
    }
}
