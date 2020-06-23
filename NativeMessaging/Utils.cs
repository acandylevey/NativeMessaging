using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace NativeMessaging
{
    internal static class Utils
    {
        public static string MessageLogLocation {
        get {
                return Path.Combine(AssemblyLoadDirectory(), "native-messaging.log");
            }
        }

        static public string AssemblyLoadDirectory()
        {
            string codeBase = Assembly.GetEntryAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        static public string AssemblyExecuteablePath()
        {
            string codeBase = Assembly.GetEntryAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            return Uri.UnescapeDataString(uri.Path);
        }

        public static void LogMessage(string msg)
        {
            LogMessage(new string[] { msg });
        }

        public static void LogMessage(string[] msgs)
        {
            try
            {
                File.AppendAllLines(MessageLogLocation, msgs);
            } catch (IOException)
            {
                Console.WriteLine("Could Not Log To File");
                //Supress Exception
            }
        }
    }
}
