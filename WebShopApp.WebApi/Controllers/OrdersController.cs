using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.Business.Operations.Order;
using WebShopApp.Business.Operations.Order.Dtos;
using WebShopApp.WebApi.Models;

namespace WebShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderRequest data)
        {
            var addOrderDto = new AddOrderDto
            {
                CustomerId = data.CustomerId,
                Quentity = data.Quentity,
                ProductIds = data.ProductIds
            };

            var result = await _orderService.AddOrder(addOrderDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
            {
                return Ok(result.Message);
            }
        }
    }
}