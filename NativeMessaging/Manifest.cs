using Newtonsoft.Json;

namespace NativeMessaging
{
    internal class Manifest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("path")]
        public string ExecuteablePath { get; set; }

        [JsonProperty("type")]
        public string Type { get { return "stdio"; } }

        [JsonProperty("allowed_origins")]
        public string[] AllowedOrigins { get; set; }

        public Manifest(string hostname, string description, string executeablePath, string[] allowedOrigins)
        {
            Name = hostname;
            Description = description;
            AllowedOrigins = allowedOrigins;
            ExecuteablePath = executeablePath;
        }
    }
}
