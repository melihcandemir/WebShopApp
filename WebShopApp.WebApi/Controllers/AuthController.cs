using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.Business.Operations.User;
using WebShopApp.Business.Operations.User.Dtos;
using WebShopApp.WebApi.Models;

namespace WebShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // kayıt olma işlemi
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } // TODO: ilerde action filter olarak kodlanacak.

            // business katmanına veriler aktarılacak
            var addUserDto = new AddUserDto
            {
                Email = data.Email,
                Password = data.Password,
                FirstName = data.FirstName,
                LastName = data.LastName,
                PhoneNumber = data.PhoneNumber
            };

            var result = await _userService.AddUser(addUserDto);

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