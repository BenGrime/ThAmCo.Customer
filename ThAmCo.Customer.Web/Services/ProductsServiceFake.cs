using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace ThAmCo.Customer.Web.Services
{

    public class ProductsServiceFake : IProductService
    {
        private readonly ProductDto[] _products =
        {
            new ProductDto { Id = 1, Name = "Tony Stark's Sunglasses", Description = "Stylish sunglasses equipped with advanced AI systems", BrandName = "Stark Industries", BrandDescription = "Technology and innovation", CategoryName = "Accessories", CategoryDescription = "Fashionable accessories", InStock = true, Price = 199.99 },
            new ProductDto { Id = 2, Name = "Wayne Enterprises Tactical Gloves", Description = "Lightweight gloves with enhanced grip and tactical sensors", BrandName = "Wayne Enterprises", BrandDescription = "Global leader in technology", CategoryName = "Apparel", CategoryDescription = "Clothing and apparel", InStock = true, Price = 149.99 },
            new ProductDto { Id = 3, Name = "Pym Particle Backpack", Description = "Advanced backpack with the ability to resize its capacity", BrandName = "Pym Technologies", BrandDescription = "Innovators in particle manipulation", CategoryName = "Luggage", CategoryDescription = "Travel and luggage", InStock = true, Price = 299.99 },
            new ProductDto { Id = 4, Name = "Stark Arc Reactor Power Bank", Description = "Portable power bank modeled after the Arc Reactor, providing unlimited energy", BrandName = "Stark Industries", BrandDescription = "Technology and innovation", CategoryName = "Electronics", CategoryDescription = "Electronic devices", InStock = false, Price = 249.99 },
            new ProductDto { Id = 5, Name = "Wakandan Vibranium Shield", Description = "Durable shield crafted from Wakandan vibranium, lightweight yet indestructible", BrandName = "Wakandan Tech", BrandDescription = "Advanced Wakandan technology", CategoryName = "Defense Gear", CategoryDescription = "Defensive equipment", InStock = false, Price = 399.99 },
            new ProductDto { Id = 6, Name = "Stark Nano-Tech Suitcase", Description = "High-tech suitcase with built-in nanotechnology for personal armor storage", BrandName = "Stark Industries", BrandDescription = "Technology and innovation", CategoryName = "Luggage", CategoryDescription = "Travel and luggage", InStock = true, Price = 499.99 }
        };


        public Task<ProductDto?> GetProductAsync(int id) //get specific product using ID
        {
            var product = _products.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(product);
        }

        public Task<IEnumerable<ProductDto>> GetProductsAsync() // get all products
        {
            var products = _products.AsEnumerable();
            return Task.FromResult(products);
        }
    }
}