using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ThAmCo.Customer.Web.Services
{
    public class ProductsService : IProductService
    {
        private readonly HttpClient _client;
        private readonly ProductDto[] _products;
        string? baseAddress = Environment.GetEnvironmentVariable("Base_Address");
        public ProductsService(HttpClient client)
        {
            // FIXME: don't hardcode base URLs -- see later lectures and examples
            
            client.BaseAddress = baseAddress != null ? new Uri(baseAddress) : new Uri("https://localhost:5001/api/");
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }
        public async Task<ProductDto?> GetProductAsync(int id)
        {
            var product = _products.FirstOrDefault(r => r.Id == id);
            return await Task.FromResult(product);
        }
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = _products.AsEnumerable();
            for(int i = 0; i < _products.Length; i++){
                products = (IEnumerable<ProductDto>)_products[i];
            }
            return await Task.FromResult(products);
        }

        
    }
}