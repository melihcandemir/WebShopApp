using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.Business.Operations.Product;
using WebShopApp.Business.Operations.Product.Dtos;
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
        [Authorize(Roles = "Admin")]
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


        [HttpPatch("{id}/price")]
        public async Task<IActionResult> PriceUpdate(int id, decimal changeBy)
        {
            var result = await _productService.PriceUpdate(id, changeBy);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok(result.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProdut(int id)
        {
            var result = await _productService.DeleteProdut(id);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok(result.Message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest data)
        {
            var updateProductDto = new UpdateProductDto
            {
                Id = id,
                ProductName = data.ProductName,
                Price = data.Price,
                StockQuantity = data.StockQuantity
            };

            var result = await _productService.UpdateProduct(updateProductDto);

            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            else
            {
                return Ok(result.Message);
            }
        }
    }
}