
using Newtonsoft.Json;

namespace OrdersManagement.Application.Services.Response
{
    public class DeviceCodeResponse
    {
        [JsonProperty("user_code")]
        public string UserCode { get; set; }

        [JsonProperty("device_code")]
        public string DeviceCode { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("interval")]
        public int Interval { get; set; }

        [JsonProperty("verification_uri")]
        public string VerificationUri { get; set; }

        [JsonProperty("verification_uri_complete")]
        public string VerificationUriComplete { get; set; }
    }
}
