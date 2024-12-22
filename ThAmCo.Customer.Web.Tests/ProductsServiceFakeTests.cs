using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Customer.Web.Services;
using Xunit;

namespace ThAmCo.Customer.Web.Tests.Services
{
    public class ProductsServiceFakeTests
    {
        private readonly ProductsServiceFake _service;

        public ProductsServiceFakeTests()
        {
            _service = new ProductsServiceFake();
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsAllProducts()
        {
            var products = await _service.GetProductsAsync();
            Assert.NotNull(products);
            Assert.Equal(6, products.Count());
        }

        [Fact]
        public async Task GetProductAsync_ReturnsProductById()
        {
            var product = await _service.GetProductAsync(1);
            Assert.NotNull(product);
            Assert.Equal(1, product.Id);
        }

        [Fact]
        public async Task GetProductAsync_ReturnsNullForInvalidId()
        {
            var product = await _service.GetProductAsync(999);
            Assert.Null(product);
        }
    }
}