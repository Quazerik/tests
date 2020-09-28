using System.ComponentModel.DataAnnotations;
using TestsApp.Enums;

namespace TestsApp.ViewModels.ToServer
{
    public class LoginViewModel
    {
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        public Role Role { get; set; }
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
