using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThAmCo.Customer.Web.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto?> GetProductAsync(int id);
    }
}