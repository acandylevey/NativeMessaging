# NativeMessaging
C# Chome Native Messaging Library

Can be used to receive data from or talk to a Chrome extension.

## Usage
Extend the host class and decide what you want to do when you receive a message.
```C#
public class MyHost : Host
    {
        private const bool SendConfirmationReceipt = true;

        public override string Hostname
        {
            get { return "com.anewtonlevey.myhost"; }
        }

        public MyHost() : base(SendConfirmationReceipt)
        {

        }

        protected override void ProcessReceivedMessage(JObject data)
        {
            SendMessage(data);
        }
    }
```

```C#
class Program
    {
        static public string AssemblyLoadDirectory
        {
            get
            {
                string codeBase = Assembly.GetEntryAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

		static public string AssemblyExecuteablePath
		{
			get
			{
				string codeBase = Assembly.GetEntryAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				return Uri.UnescapeDataString(uri.Path);
			}
		}

        static Host Host;

        static string[] AllowedOrigins = new string[] { "chrome-extension://knldjmfmopnpolahpmmgbagdohdnhkik/" };
        static string Description = "Description Goes Here";

        static void Main(string[] args)
        {
            Host = new MyHost();
            if (args.Contains("--register"))
            {
                Host.GenerateManifest(Description, AllowedOrigins);
                Host.Register();
            } else if(args.Contains("--unregister"))
            {
                Host.UnRegister();
            } else
            {
                Host.Listen();
            }
        }
    }
```
