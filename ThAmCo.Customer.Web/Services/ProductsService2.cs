using System.Net.Http.Headers;
using System;
using System.Text.Json;
using System.Net;

namespace ThAmCo.Customer.Web.Services
{
    public class ProductsService2 : IProductService
    {
        // private readonly HttpClient _client;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        string? clientId; 
        string? clientSecret; 

        public ProductsService2(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            // var baseAddress = "https://thamcoproductsapiv1.azurewebsites.net/products";//configuration["Services:Values:BaseAddress"];
            // if (string.IsNullOrEmpty(baseAddress))
            // {
            //     throw new ArgumentNullException(nameof(baseAddress), "Base address configuration is missing.");
            // }
            // client.BaseAddress = new Uri(baseAddress);
            // _client = client;
            // _configuration = configuration;

            _clientFactory = clientFactory;
            _configuration = configuration;
            // clientId = Environment.GetEnvironmentVariable("ClientId");
            // clientSecret = Environment.GetEnvironmentVariable("ClientSecret");
        }
       record TokenDto(string access_token, string token_type, int expires_in);

       private const string TokenFilePath = "token_cache.json";

        private async Task SaveTokenAsync(string token, DateTime expiration)
        {
            var tokenData = new TokenData { AccessToken = token, Expiration = expiration };
            var json = JsonSerializer.Serialize(tokenData);
            await File.WriteAllTextAsync(TokenFilePath, json);
        }

        private async Task<(string? Token, DateTime Expiration)> LoadTokenAsync()
        {
            if (!File.Exists(TokenFilePath))
            {
                return (null, DateTime.MinValue);
            }

            var json = await File.ReadAllTextAsync(TokenFilePath);
            var tokenData = JsonSerializer.Deserialize<TokenData>(json);
            return (tokenData?.AccessToken, tokenData?.Expiration ?? DateTime.MinValue);
        }

        private class TokenData
        {
            public string? AccessToken { get; set; }
            public DateTime Expiration { get; set; }
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var (cachedToken, tokenExpiration) = await LoadTokenAsync();

            if (cachedToken != null && tokenExpiration > DateTime.UtcNow)
            {
                return cachedToken;
            }

            var tokenClient = _clientFactory.CreateClient();
            var authBaseAddress = _configuration["Auth:Authority"]; // _configuration["Auth:Authority"]; "https://ben-grime.uk.auth0.com"
            tokenClient.BaseAddress = new Uri(authBaseAddress);
            var tokenParams = new Dictionary<string, string> {
                { "grant_type", "client_credentials" },
                { "client_id", _configuration["Auth:ClientId"] }, // clientId }, "SvN5f6uE7LLwM8N19NDZgfMLYv3LnKTz"
                { "client_secret",  _configuration["Auth:ClientSecret"]}, // clientSecret }, "Sr7tJSjLcIDmDIdY1BQgjcsFQ_G4i0dhWioKCs8VUTdUsF9PksPttDyR-FYZqj98"
                { "audience",  _configuration["Services:Values:AuthAudience"] }, // _configuration["Services:Values:AuthAudience"] },
            };
            var tokenForm = new FormUrlEncodedContent(tokenParams);
            var tokenResponse = await tokenClient.PostAsync("oauth/token", tokenForm);
            tokenResponse.EnsureSuccessStatusCode();
            var tokenInfo = await tokenResponse.Content.ReadFromJsonAsync<TokenDto>();

            var newToken = tokenInfo?.access_token;
            var newExpiration = DateTime.UtcNow.AddSeconds(tokenInfo?.expires_in ?? 0);

            if (newToken == null)
            {
                throw new Exception("Failed to retrieve access token.");
            }
            await SaveTokenAsync(newToken, newExpiration);

            return newToken;
        }

        public async Task<ProductDto?> GetProductAsync(int id)
        {
            var tokenClient = _clientFactory.CreateClient();
            var accessToken = await GetAccessTokenAsync();
            var client = _clientFactory.CreateClient();
            var serviceBaseAddress = _configuration["Services:Values:BaseAddress"];//_configuration["Services:Values:BaseAddress"];
            client.BaseAddress = new Uri(serviceBaseAddress);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("products/"+id);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
        
            if (response.IsSuccessStatusCode)
            {   
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response body: {responseBody}");
                var product = await response.Content.ReadFromJsonAsync<ProductDto>();
                if (product == null)
                {
                    throw new Exception("Failed to deserialize product list.");
                }
                return product;
            }
            else
            {
                throw new Exception("Failed to retrieve product.");
            }
            
        }
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var tokenClient = _clientFactory.CreateClient();
            var accessToken = await GetAccessTokenAsync();
            var client = _clientFactory.CreateClient();
            var serviceBaseAddress = _configuration["Services:Values:BaseAddress"];//_configuration["Services:Values:BaseAddress"];
            client.BaseAddress = new Uri(serviceBaseAddress);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);//tokenInfo?.access_token
            var response = await client.GetAsync("products");
            response.EnsureSuccessStatusCode();
            if(response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                if (products == null)
                {
                    throw new Exception("Failed to deserialize product list.");
                }
                return products;
            }
            else
            {
                throw new Exception("Failed to retrieve product.");
            }
        }
    }
}