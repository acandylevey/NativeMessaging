namespace NativeMessaging
{
    /// <summary>
    /// Controls the logging behavior of the application
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// The path to the generated log file.
        /// </summary>
        public static string MessageLogLocation
        {
            get
            {
                return Path.Combine(
                    Utils.AssemblyLoadDirectory() ?? "", 
                    "native-messaging.log");
            }
        }
        /// <summary>
        /// Activate the logging if set to <see langword="true"/>
        /// </summary>
        public static bool Active
        {
            get;
            set;
        } = false;

        internal static void LogMessage(string msg)
        {
            if (!Active)
            {
                return;
            }

            try
            {
                File.AppendAllText(
                    MessageLogLocation, 
                    msg + Environment.NewLine);
            }
            catch (IOException)
            {
                Console.WriteLine("Could Not Log To File");
                //Supress Exception
            }
        }
    }
}
