// using System;
// using System.Collections.Generic;
// using System.Net;
// using System.Net.Http;
// using System.Threading.Tasks;
// using System.Net.Http.Headers;

// namespace ThAmCo.Customer.Web.Services
// {
//     public class ProductsService : IProductService
//     {

//         private readonly IHttpClientFactory _clientFactory;
//         private readonly IConfiguration _configuration;
//         string? baseAddress = Environment.GetEnvironmentVariable("Base_Address");
//         string? clientId = Environment.GetEnvironmentVariable("ClientId");
//         string? clientSecret = Environment.GetEnvironmentVariable("ClientSecret");
        
//         public ProductsService(IHttpClientFactory clientFactory, IConfiguration configuration)
//         {
//             _clientFactory = clientFactory;
//             _configuration = configuration;
//         }


//         record TokenDto(string access_token, string token_type, int expires_in);


//         public async Task<ProductDto?> GetProductAsync(int id)
//         {
//             var tokenClient = _clientFactory.CreateClient();

//             var authBaseAddress = _configuration["Auth:Authority"];
//             if (string.IsNullOrEmpty(authBaseAddress))
//             {
//                 throw new Exception("Auth base address is not configured.");
//             }
//             tokenClient.BaseAddress = new Uri(authBaseAddress);

//             var tokenParams = new Dictionary<string, string> {
//                 { "grant_type", "client_credentials" },
//                 { "client_id",clientId ?? throw new ArgumentNullException("ClientId") },
//                 { "client_secret", clientSecret ?? throw new ArgumentNullException("ClientSecret") },
//                 { "audience", _configuration["Services:Values:AuthAudience"] ?? throw new ArgumentNullException("Services:Values:AuthAudience") },
//             };

//             var tokenForm = new FormUrlEncodedContent(tokenParams);
//             var tokenResponse = await tokenClient.PostAsync("oauth/token", tokenForm);
//             tokenResponse.EnsureSuccessStatusCode();
//             var tokenInfo = await tokenResponse.Content.ReadFromJsonAsync<TokenDto>();

//             var client = _clientFactory.CreateClient();
//             var serviceBaseAddress = _configuration["Services:Values:BaseAddress"];
//             if (string.IsNullOrEmpty(serviceBaseAddress))
//             {
//                 throw new Exception("Service base address is not configured.");
//             }
//             client.BaseAddress = new Uri(serviceBaseAddress);
//             client.DefaultRequestHeaders.Authorization =
//                 new AuthenticationHeaderValue("Bearer", tokenInfo?.access_token);
//             var response = await client.GetAsync("/products?id=" + id);
//              if (response.IsSuccessStatusCode)
//             {
//                 var content = response.Content;
//                 if (content == null)
//                 {
//                     throw new Exception("Response content is null.");
//                 }
//                 var product = await content.ReadFromJsonAsync<ProductDto>();
//                 return product;
//             }
//             else
//             {
//                 throw new Exception("Failed to retrieve product.");
//             }
            
//         }
//         public async Task<IEnumerable<ProductDto>> GetProductsAsync()
//         {
//             var tokenClient = _clientFactory.CreateClient();
//             var authBaseAddress = _configuration["Auth:Authority"];
//             if (string.IsNullOrEmpty(authBaseAddress))
//             {
//                 throw new Exception("Auth base address is not configured.");
//             }
//             tokenClient.BaseAddress = new Uri(authBaseAddress);
//             var tokenParams = new Dictionary<string, string> {
//                 { "grant_type", "client_credentials" },
//                 { "client_id",clientId ?? throw new ArgumentNullException("ClientId") },
//                 { "client_secret",clientSecret ?? throw new ArgumentNullException("ClientSecret") },
//                 { "audience", _configuration["Services:Values:AuthAudience"]?? throw new ArgumentNullException("Services:Values:AuthAudience") },
//             };
//             var tokenForm = new FormUrlEncodedContent(tokenParams);
//             var tokenResponse = await tokenClient.PostAsync("oauth/token", tokenForm);
//             tokenResponse.EnsureSuccessStatusCode();
//             var tokenInfo = await tokenResponse.Content.ReadFromJsonAsync<TokenDto>();


//             var client = _clientFactory.CreateClient();
//             var serviceBaseAddress = _configuration["Services:Values:BaseAddress"];
//             if (string.IsNullOrEmpty(serviceBaseAddress))
//             {
//                 throw new Exception("Service base address is not configured.");
//             }
//             client.BaseAddress = new Uri(serviceBaseAddress);
//             client.DefaultRequestHeaders.Authorization =
//                 new AuthenticationHeaderValue("Bearer", tokenInfo?.access_token);
//             var response = await client.GetAsync("/products");
//             if(response.IsSuccessStatusCode)
//             {
//                 var products = await response.Content.ReadFromJsonAsync<ProductDto[]>();
//                 return products ?? Enumerable.Empty<ProductDto>();
//             }
//             else{
//                 throw new Exception("Failed to retrieve product.");
//             }
//         }

//         //  public async Task<ProductDto?> GetProductAsync(int id)
//         // {
//         //     ProductDto[] _products =[];
//         //     var product = _products.FirstOrDefault(r => r.Id == id);
//         //     return await Task.FromResult(product);
//         // }
//         // public async Task<IEnumerable<ProductDto>> GetProductsAsync()
//         // {
//         //     ProductDto[] _products =[];
//         //     var products = _products.AsEnumerable();
//         //     for(int i = 0; i < _products.Length; i++){
//         //         products = (IEnumerable<ProductDto>)_products[i];
//         //     }
//         //     return await Task.FromResult(products);
//         // }

        
//     }
// }