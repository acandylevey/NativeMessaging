namespace NativeMessaging {
    partial class ChromiumBrowser {
        /// <summary>
        /// <see cref="ChromiumBrowser"/> object for Google Chrome.
        /// </summary>
        public static ChromiumBrowser GoogleChrome => new ChromiumBrowser("Google Chrome", "SOFTWARE\\Google\\Chrome\\");
        
        /// <summary>
        /// <see cref="ChromiumBrowser"/> object for Microsoft Edge.
        /// </summary>
        public static ChromiumBrowser MicrosoftEdge => new ChromiumBrowser("Microsoft Edge", "SOFTWARE\\Microsoft\\Edge\\");
    }
}
