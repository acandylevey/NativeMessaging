using NativeMessaging;

namespace NativeMessagingTest
{
    class Program
    {
        static Host Host;

        readonly static string[] AllowedOrigins = new string[] { "chrome-extension://knldjmfmopnpolahpmmgbagdohdnhkik/" };
        readonly static string Description = "Alexander Candy-Levey Native Messaging Example Host";

        static void Main(string[] args)
        {
            Log.Active = true;

            Host = new MyHost();
            Host.SupportedBrowsers.Add(ChromiumBrowser.GoogleChrome);
            Host.SupportedBrowsers.Add(ChromiumBrowser.MicrosoftEdge);

            if (args.Contains("--register"))
            {
                Host.GenerateManifest(Description, AllowedOrigins);
                Host.Register();
            }
            else if (args.Contains("--unregister"))
            {
                Host.Unregister();
            }
            else
            {
                Host.Listen();
            }
        }
    }
}