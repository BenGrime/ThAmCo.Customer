// using System.Collections.Generic;
// using System.Net.Http.Json;
// using System.Threading.Tasks;
// using Moq;
// using Xunit;
// using ThAmCo.Customer.Web.Services;
// using ThAmCo.Customer.Web.Models;
// using Microsoft.Extensions.Configuration;
// using System.Net;
// using System.Net.Http;
// using Moq.Protected;

// namespace ThAmCo.Customer.Web.Tests.Services
// {
//     public class ProductsServiceTests
//     {
//         private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
//         private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
//         private readonly IConfiguration _configuration;
//         private readonly ProductsService _service;

//         public ProductsServiceTests()
//         {
//             _mockHttpClientFactory = new Mock<IHttpClientFactory>();
//             _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
//             _configuration = new ConfigurationBuilder()
//                 .AddInMemoryCollection(new Dictionary<string, string?>
//                 {
//                     { "Services:Values:BaseAddress", "https://example.com" },
//                     { "Auth:Authority", "https://ben-grime.uk.auth0.com" },
//                     { "Auth:ClientId", "SvN5f6uE7LLwM8N19NDZgfMLYv3LnKTz" },
//                     { "Auth:ClientSecret", "Sr7tJSjLcIDmDIdY1BQgjcsFQ_G4i0dhWioKCs8VUTdUsF9PksPttDyR-FYZqj98" },
//                     { "Services:Values:AuthAudience", "https://secureapi.example.com" }
//                 })
//                 .Build();

//             var client = new HttpClient(_mockHttpMessageHandler.Object)
//             {
//                 BaseAddress = new System.Uri("https://example.com")
//             };

//             _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

//             _service = new ProductsService(_mockHttpClientFactory.Object, _configuration);
//         }

//         [Fact]
//         public async Task GetProductsAsync_ReturnsProducts()
//         {
//             var products = new List<ProductDto>
//             {
//                 new ProductDto { Id = 1, Name = "Product 1", Description = "Description 1", BrandName = "Brand 1", BrandDescription = "Brand Description 1", CategoryName = "Category 1", CategoryDescription = "Category Description 1" },
//                 new ProductDto { Id = 2, Name = "Product 2", Description = "Description 2", BrandName = "Brand 2", BrandDescription = "Brand Description 2", CategoryName = "Category 2", CategoryDescription = "Category Description 2" }
//             };

//             _mockHttpMessageHandler.Protected()
//                 .Setup<Task<HttpResponseMessage>>(
//                     "SendAsync",
//                     ItExpr.IsAny<HttpRequestMessage>(),
//                     ItExpr.IsAny<CancellationToken>()
//                 )
//                 .ReturnsAsync(new HttpResponseMessage
//                 {
//                     StatusCode = HttpStatusCode.OK,
//                     Content = JsonContent.Create(products)
//                 });

//             var result = await _service.GetProductsAsync();

//             Assert.NotNull(result);
//             Assert.Equal(2, result.Count());
//         }

//         [Fact]
//         public async Task GetProductAsync_ReturnsProductById()
//         {
//             var product = new ProductDto { Id = 1, Name = "Product 1", Description = "Description 1", BrandName = "Brand 1", BrandDescription = "Brand Description 1", CategoryName = "Category 1", CategoryDescription = "Category Description 1" };

//             _mockHttpMessageHandler.Protected()
//                 .Setup<Task<HttpResponseMessage>>(
//                     "SendAsync",
//                     ItExpr.IsAny<HttpRequestMessage>(),
//                     ItExpr.IsAny<CancellationToken>()
//                 )
//                 .ReturnsAsync(new HttpResponseMessage
//                 {
//                     StatusCode = HttpStatusCode.OK,
//                     Content = JsonContent.Create(product)
//                 });

//             var result = await _service.GetProductAsync(1);

//             Assert.NotNull(result);
//             Assert.Equal(1, result.Id);
//         }

//         [Fact]
//         public async Task GetProductAsync_ReturnsNullForInvalidId()
//         {
//             _mockHttpMessageHandler.Protected()
//                 .Setup<Task<HttpResponseMessage>>(
//                     "SendAsync",
//                     ItExpr.IsAny<HttpRequestMessage>(),
//                     ItExpr.IsAny<CancellationToken>()
//                 )
//                 .ReturnsAsync(new HttpResponseMessage
//                 {
//                     StatusCode = HttpStatusCode.NotFound
//                 });

//             var result = await _service.GetProductAsync(999);

//             Assert.Null(result);
//         }
//     }
// }