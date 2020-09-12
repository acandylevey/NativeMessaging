using NativeMessaging;
using Newtonsoft.Json.Linq;

namespace NativeMessagingTest
{
    public class MyHost : Host
    {
        // This can currently be used with the example app provided here:
        // https://chromium.googlesource.com/chromium/src/+/master/chrome/common/extensions/docs/examples/api/nativeMessaging

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
