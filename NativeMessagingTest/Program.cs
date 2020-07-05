using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NativeMessaging;

namespace NativeMessagingTest {
    class Program {
        static public string AssemblyLoadDirectory {
            get {
                string codeBase = Assembly.GetEntryAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        static public string AssemblyExecuteablePath {
            get {
                string codeBase = Assembly.GetEntryAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                return Uri.UnescapeDataString(uri.Path);
            }
        }

        static Host Host;

        static string[] AllowedOrigins = new string[] { "chrome-extension://knldjmfmopnpolahpmmgbagdohdnhkik/" };
        static string Description = "Description Goes Here";

        static void Main(string[] args) {
            Host = new MyHost();
            if (args.Contains("--register")) {
                Host.GenerateManifest(Description, AllowedOrigins);
                Host.Register();
            } else if (args.Contains("--unregister")) {
                Host.UnRegister();
            } else {
                Host.Listen();
            }
        }
    }
}
