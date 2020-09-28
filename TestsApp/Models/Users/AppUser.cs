using Microsoft.AspNetCore.Identity;

namespace TestsApp.Models.Users
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
