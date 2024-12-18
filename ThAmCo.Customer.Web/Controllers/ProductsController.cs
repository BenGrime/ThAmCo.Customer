using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ThAmCo.Customer.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ThAmCo.Customer.Web.Controllers;
// [Authorize]
public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        // GET: /products/
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductDto> products;
            try
            {
                products = await _productService.GetProductsAsync();
            }
            catch
            {
                _logger.LogWarning("Exception occurred using products service.");
                products = new List<ProductDto>();
            }
            return View(products);
        }

        // GET: /products/details/{id}
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                var product = await _productService.GetProductAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            catch
            {
                _logger.LogWarning("Exception occurred using products service.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    
}