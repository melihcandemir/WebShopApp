using Microsoft.AspNetCore.Mvc;
using WebShopApp.Business.Operations.User;
using WebShopApp.Business.Operations.User.Dtos;
using WebShopApp.WebApi.Models;

namespace WebShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductRequest data)
        {
            var addProductDto = new AddProductDto
            {
                ProductName = data.ProductName,
                Price = data.Price,
                StockQuantity = data.StockQuantity
            };

            var result = await _productService.AddProduct(addProductDto);

            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }

        }
    }
}