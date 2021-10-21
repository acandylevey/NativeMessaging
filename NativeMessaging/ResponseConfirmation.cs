using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NativeMessaging
{
    internal class ResponseConfirmation
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public JObject Data { get; set; }

        public ResponseConfirmation(JObject data)
        {
            Data = data;
            Message = "Confirmation of received data";
        }

        public JObject GetJObject()
        {
            return JsonConvert.DeserializeObject<JObject>(
                JsonConvert.SerializeObject(this));
        }
    }
}