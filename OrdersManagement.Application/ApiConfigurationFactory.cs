
using Microsoft.Extensions.Configuration;

namespace OrdersManagement.Application
{
    public static class ApiConfiguration
    {
        private const string BaseUrlSectionName = "AllegroApi:BaseUrl";
        private const string AccessTokenSectionName = "AllegroApi:AccessToken";
        private const string DefaultConnectionStringSectionName = "DefaultConnection";

        private static IConfiguration _configuration;

        public static string token;
        public static string baseUrl;
        public static string connectionString;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;

            token = _configuration[AccessTokenSectionName];
            baseUrl = _configuration[BaseUrlSectionName];
            connectionString = _configuration.GetConnectionString(DefaultConnectionStringSectionName);
        }
    }
}
