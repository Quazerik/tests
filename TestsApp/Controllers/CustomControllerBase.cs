using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestsApp.Models.Users;

namespace TestsApp.Controllers
{
    [ApiController]
    public abstract class CustomControllerBase : ControllerBase
    {
        readonly protected UserManager<AppUser> _userManager;

        protected CustomControllerBase(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // not working yet
        protected string GetCurrentUserId()
        {
            return _userManager.GetUserId(User);
        }

        //not working yet
        protected async Task<AppUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}
