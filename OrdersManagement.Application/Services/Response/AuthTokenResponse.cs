using Newtonsoft.Json;

namespace OrdersManagement.Application.Services.Response
{
    public class AuthTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("allegro_api")]
        public bool AllegroApi { get; set; }

        [JsonProperty("jti")]
        public string Jti { get; set; }
    }
}
