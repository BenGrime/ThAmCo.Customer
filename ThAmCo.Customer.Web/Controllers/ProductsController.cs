using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ThAmCo.Customer.Web.Services;

namespace ThAmCo.Customer.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ILogger _logger;
    private readonly IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productsService)
    {
        _logger = logger;
        _productService = productsService;
    }
    
    // GET: /products/
    public async Task<IActionResult> Index([FromQuery] string? subject)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        IEnumerable<ProductDto> products = null!;
        try
        {
            products = await _productService.GetProductsAsync();
        }
        catch
        {
            _logger.LogWarning("Exception occurred using products service.");
            products = Array.Empty<ProductDto>();
        }
        return View(products.ToList());

    }


    // GET: /products/details/{id}
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        try
        {
            var products = await _productService.GetProductAsync(id.Value);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
        catch
        {
            _logger.LogWarning("Exception occurred using Products service.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
}