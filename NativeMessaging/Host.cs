using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NativeMessaging {
    /// <summary>
    /// Abstract class that should be extended to communicate with Chrome
    /// </summary>
    public abstract class Host {
        private readonly bool SendConfirmationReceipt;
        private readonly string ManifestPath;

        /// <summary>
        /// Name of the Native Messaging Host
        /// </summary>
        public abstract string Hostname { get; }

        /// <summary>
        /// List of supported chromium Browsers.
        /// </summary>
        public List<ChromiumBrowser> SupportedBrowsers { get; }

        /// <summary>
        /// Creates the Host Object
        /// </summary>
        /// <param name="sendConfirmationReceipt"><see langword="true" /> for the host to automatically send message confirmation receipt.</param>
        public Host(bool sendConfirmationReceipt = true) {
            SupportedBrowsers = new List<ChromiumBrowser>(2);

            SendConfirmationReceipt = sendConfirmationReceipt;
            ManifestPath = Path.Combine(Utils.AssemblyLoadDirectory(), Hostname + "-manifest.json");
        }

        /// <summary>
        /// Starts listening for input.
        /// </summary>
        public void Listen() {
            if (!IsRegistered())
                throw new NotRegisteredWithBrowserException(Hostname);

            JObject data;
            while ((data = Read()) != null) {
                Log.LogMessage("Data Received:" + JsonConvert.SerializeObject(data));

                if (SendConfirmationReceipt)
                    SendMessage(new ResponseConfirmation(data).GetJObject());
                ProcessReceivedMessage(data);
            }
        }

        private JObject Read() {
            Log.LogMessage("Waiting for Data");

            var stdin = Console.OpenStandardInput();

            var lengthBytes = new byte[4];
            stdin.Read(lengthBytes, 0, 4);

            var buffer = new char[BitConverter.ToInt32(lengthBytes, 0)];

            using (var reader = new StreamReader(stdin))
                while (reader.Peek() >= 0)
                    reader.Read(buffer, 0, buffer.Length);

            return JsonConvert.DeserializeObject<JObject>(new string(buffer));
        }

        /// <summary>
        /// Sends a message to Chrome, note that the message might not be able to reach Chrome if the stdIn / stdOut aren't properly configured (i.e. Process needs to be started by Chrome)
        /// </summary>
        /// <param name="data">A <see cref="JObject"/> containing the data to be sent.</param>
        public void SendMessage(JObject data) {
            Log.LogMessage("Sending Message:" + JsonConvert.SerializeObject(data));

            var bytes = System.Text.Encoding.UTF8.GetBytes(data.ToString(Formatting.None));
            var stdout = Console.OpenStandardOutput();
            stdout.WriteByte((byte)((bytes.Length >> 0) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 8) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 16) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 24) & 0xFF));
            stdout.Write(bytes, 0, bytes.Length);
            stdout.Flush();
        }

        /// <summary>
        /// Override this method in your extended <see cref="Host"/> to process messages received from Chrome.
        /// </summary>
        /// <param name="data">A <see cref="JObject"/> containing the data received.</param>
        protected abstract void ProcessReceivedMessage(JObject data);

        /// <summary>
        /// Generates the manifest and saves it to the correct location.
        /// </summary>
        /// <param name="description">Short application description to be included in the manifest.</param>
        /// <param name="allowedOrigins">List of extensions that should have access to the native messaging host.<br />Wildcards such as <code>chrome-extension://*/*</code> are not allowed.</param>
        /// <param name="overwrite">Determines if the manifest should be overwritten if it already exists.<br />Defaults to <see langword="false"/>.</param>
        public void GenerateManifest(string description, string[] allowedOrigins, bool overwrite = false) {
            if (File.Exists(ManifestPath) && !overwrite) {
                Log.LogMessage("Manifest exists already");
            } else {
                Log.LogMessage("Generating Manifest");

                var manifest = JsonConvert.SerializeObject(new Manifest(Hostname, description, Utils.AssemblyExecuteablePath(), allowedOrigins));
                File.WriteAllText(ManifestPath, manifest);

                Log.LogMessage("Manifest Generated");
            }
        }

        #region Browser Registration
        /// <summary>
        /// Checks if the host is registered with all required browsers.
        /// </summary>
        /// <returns><see langword="true"/> if the required information is present in the registry.</returns>
        public bool IsRegistered() {
            bool result = false;

            foreach (ChromiumBrowser browser in SupportedBrowsers) {
                result = result || browser.IsRegistered(Hostname, ManifestPath);
            }

            return result;
        }

        /// <summary>
        /// Register the application to open with all required browsers.
        /// </summary>
        public void Register() {
            foreach (ChromiumBrowser browser in SupportedBrowsers) {
                browser.Register(Hostname, ManifestPath);
            }
        }

        /// <summary>
        /// De-register the application to open with all required browsers.
        /// </summary>
        public void Unregister() {
            foreach (ChromiumBrowser browser in SupportedBrowsers) {
                browser.Unregister(Hostname);
            }
        }
        #endregion
    }
}