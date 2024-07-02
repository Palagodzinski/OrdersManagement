using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrdersManagement.Application.Interfaces;
using OrdersManagement.Application.Services.Response;
using System.Net.Http.Headers;
using System.Text;

namespace OrdersManagement.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IConfiguration _configuration;

        private readonly string authTokenUrl;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string deviceAuthUrl;            

        public AuthorizationService(IConfiguration configuration)
        {
            _configuration = configuration;
            authTokenUrl = _configuration["AllegroApi:AuthTokenUrl"]!;
            clientId = _configuration["AllegroApi:ClientId"]!;
            clientSecret = _configuration["AllegroApi:ClientSecret"]!;
            deviceAuthUrl = _configuration["AllegroApi:DeviceAuthUrl"]!;
        }

        public async Task<string> GetAuthorizationTokenAsync()
        {
            try
            {               
                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

                var deviceCode = await GetDeviceCodeAsync(clientId, clientSecret);

                using (var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, authTokenUrl)
                    {
                        Content = new StringContent($"grant_type=urn:ietf:params:oauth:grant-type:device_code&device_code={deviceCode}", Encoding.UTF8)
                    };

                    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                    var response = await httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Error occured while requesting for authorization token - StatusCode={response.StatusCode} Reason={response.ReasonPhrase}");
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var authTokenResponse = JsonConvert.DeserializeObject<AuthTokenResponse>(content);

                    if (authTokenResponse is null)
                    {
                        throw new Exception("Error occured during deserialization of authorization token");
                    }

                    return authTokenResponse.AccessToken;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<DeviceCodeResponse> GetDeviceCodeAsync(string clientId, string clientSecret)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, deviceAuthUrl)
                {
                    Content = new StringContent($"client_id={clientId}", Encoding.UTF8, "application/x-www-form-urlencoded")
                };

                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to get device code - StatusCode={response.StatusCode} Reason={response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var deviceCode = JsonConvert.DeserializeObject<DeviceCodeResponse>(content);

                if (deviceCode is null)
                {
                    throw new Exception($"Problem occured while trying to deserialize content of device code reponse");
                }

                return deviceCode;
            }
        }
    }
}
