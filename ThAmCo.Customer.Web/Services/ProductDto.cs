using System;

namespace ThAmCo.Customer.Web.Services
{
    public class ProductDto
    {
        public int BrandId { get; set; }

        public string?BrandName { get; set; }

        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public int Id { get; set; }

        public string? Name { get; set; }

        public Boolean inStock { get; set; }

        public double price { get; set; }
    }
}