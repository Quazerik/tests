using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestsApp.Models.Users;
using TestsApp.Services;
using TestsApp.ViewModels.ToServer;

namespace TestsApp.Controllers
{
    public class AccountController : CustomControllerBase
    {
        private readonly TokenService _tokenService;
        private IConfiguration _config;

        public AccountController(UserManager<AppUser> userManager, TokenService tokenService, IConfiguration config) :
            base(userManager)
        {
            _tokenService = tokenService;
            _config = config;
        }

        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            var dbUser = await _userManager.FindByEmailAsync(login.Email);
            if (dbUser == null)
            {
                return NotFound("Неверный адрес электронной почты");
            }

            var isValid = await _userManager.CheckPasswordAsync(dbUser, login.Password);
            if (!isValid)
            {
                return NotFound("Неверный пароль");
            }

            var token = _tokenService.BuildToken(dbUser, _config["Jwt:Key"], _config["Jwt:Issuer"]);
            return Ok(token);
        }

        [HttpPost("api/logout")]
        public IActionResult Logout()
        {
            //Удаление данных аутентификации на фронте

            return SignOut();
        }
    }
}
