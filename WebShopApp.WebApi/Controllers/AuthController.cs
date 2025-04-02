using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WebShopApp.Business.Operations.User;
using WebShopApp.Business.Operations.User.Dtos;
using WebShopApp.WebApi.Jwt;
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
        [HttpPost("register")]
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

        // login
        [HttpPost("login")]
        public IActionResult Login(LoginRequest data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _userService.LoginUser(new LoginUserDto
            {
                Email = data.Email,
                Password = data.Password
            });

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }

            // bilgiler doğru ise --> jwt

            var user = result.Data;
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>(); // tüm bilgileri tutan yapı

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
            });

            return Ok(new LoginResponse
            {
                Message = "Giriş başarılı",
                Token = token
            });
        }

    }
}