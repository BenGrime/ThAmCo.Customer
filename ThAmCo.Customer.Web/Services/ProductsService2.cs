using System.Net.Http.Headers;
using System;

namespace ThAmCo.Customer.Web.Services
{
    public class ProductsService2 : IProductService
    {
        // private readonly HttpClient _client;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        string? clientId = Environment.GetEnvironmentVariable("ClientId");
        string? clientSecret = Environment.GetEnvironmentVariable("ClientSecret");

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
        }
       record TokenDto(string access_token, string token_type, int expires_in);

        public async Task<ProductDto?> GetProductAsync(int id)
        {
            var tokenClient = _clientFactory.CreateClient();
            var authBaseAddress = "https://ben-grime.uk.auth0.com";//_configuration["Auth:Authority"];
            tokenClient.BaseAddress = new Uri(authBaseAddress);
            var tokenParams = new Dictionary<string, string> {
                 { "grant_type", "client_credentials" },
                { "client_id","SvN5f6uE7LLwM8N19NDZgfMLYv3LnKTz"},//clientId },
                { "client_secret","Sr7tJSjLcIDmDIdY1BQgjcsFQ_G4i0dhWioKCs8VUTdUsF9PksPttDyR-FYZqj98"},//clientSecret},
                { "audience", "https://secureapi.example.com"},//_configuration["Services:Values:AuthAudience"] },
            };
            var tokenForm = new FormUrlEncodedContent(tokenParams);
            var tokenResponse = await tokenClient.PostAsync("oauth/token", tokenForm);
            tokenResponse.EnsureSuccessStatusCode();
            var tokenInfo = await tokenResponse.Content.ReadFromJsonAsync<TokenDto>();
            // FIXME: token should be cached rather than obtained each call
            var client = _clientFactory.CreateClient();
            var serviceBaseAddress = "https://thamcoproductsapiv1.azurewebsites.net/products";//_configuration["Services:Values:BaseAddress"];
            client.BaseAddress = new Uri(serviceBaseAddress);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenInfo?.access_token);
            var response = await client.GetAsync("products/"+id);//broken
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
            var authBaseAddress = "https://ben-grime.uk.auth0.com";//_configuration["Auth:Authority"];
            tokenClient.BaseAddress = new Uri(authBaseAddress);
            var tokenParams = new Dictionary<string, string> {
                { "grant_type", "client_credentials" },
                { "client_id","SvN5f6uE7LLwM8N19NDZgfMLYv3LnKTz"},//clientId },
                { "client_secret","Sr7tJSjLcIDmDIdY1BQgjcsFQ_G4i0dhWioKCs8VUTdUsF9PksPttDyR-FYZqj98"},//clientSecret},
                { "audience", "https://secureapi.example.com"},//_configuration["Services:Values:AuthAudience"] },
            };
            var tokenForm = new FormUrlEncodedContent(tokenParams);
            var tokenResponse = await tokenClient.PostAsync("oauth/token", tokenForm);
            tokenResponse.EnsureSuccessStatusCode();
            var tokenInfo = await tokenResponse.Content.ReadFromJsonAsync<TokenDto>();
            // FIXME: token should be cached rather than obtained each call
            var client = _clientFactory.CreateClient();
            var serviceBaseAddress = "https://thamcoproductsapiv1.azurewebsites.net/products";//_configuration["Services:Values:BaseAddress"];
            client.BaseAddress = new Uri(serviceBaseAddress);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenInfo?.access_token);
            var response = await client.GetAsync("products");
            response.EnsureSuccessStatusCode();
            if(response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<ProductDto[]>();
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