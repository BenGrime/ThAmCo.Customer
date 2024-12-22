using System;

namespace ThAmCo.Customer.Web.Services
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set;}
        public required string BrandName { get; set; }
        public required string BrandDescription { get; set; }
        public required string CategoryName { get; set; }
        public required string CategoryDescription { get; set; }
        public bool InStock { get; set; }
        public double Price { get; set; }
    }
}