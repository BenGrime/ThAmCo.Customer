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
            new ProductDto {Id = 1, Name = "Tony Stark's Sunglasses", Description = "Stylish sunglasses equipped with advanced AI systems", BrandId = 101, BrandName = "Stark Industries", CategoryId = 201, CategoryName = "Accessories", inStock = true, price = 199.99 },
            new ProductDto {Id = 2, Name = "Wayne Enterprises Tactical Gloves", Description = "Lightweight gloves with enhanced grip and tactical sensors", BrandId = 102, BrandName = "Wayne Enterprises", CategoryId = 202, CategoryName = "Apparel", inStock = true, price = 149.99 },

new ProductDto 
{ 
    Id = 3, 
    Name = "Pym Particle Backpack", 
    Description = "Advanced backpack with the ability to resize its capacity", 
    BrandId = 103, 
    BrandName = "Pym Technologies", 
    CategoryId = 203, 
    CategoryName = "Luggage", 
    inStock = true, 
    price = 299.99 
},

new ProductDto 
{ 
    Id = 4, 
    Name = "Stark Arc Reactor Power Bank", 
    Description = "Portable power bank modeled after the Arc Reactor, providing unlimited energy", 
    BrandId = 101, 
    BrandName = "Stark Industries", 
    CategoryId = 204, 
    CategoryName = "Electronics", 
    inStock = false, 
    price = 249.99 
},

new ProductDto 
{ 
    Id = 5, 
    Name = "Wakandan Vibranium Shield", 
    Description = "Durable shield crafted from Wakandan vibranium, lightweight yet indestructible", 
    BrandId = 104, 
    BrandName = "Wakandan Tech", 
    CategoryId = 205, 
    CategoryName = "Defense Gear", 
    inStock = false, 
    price = 399.99 
},

new ProductDto 
{ 
    Id = 6, 
    Name = "Stark Nano-Tech Suitcase", 
    Description = "High-tech suitcase with built-in nanotechnology for personal armor storage", 
    BrandId = 101, 
    BrandName = "Stark Industries", 
    CategoryId = 203, 
    CategoryName = "Luggage", 
    inStock = true, 
    price = 499.99 
},



                
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