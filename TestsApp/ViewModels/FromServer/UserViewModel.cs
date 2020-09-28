using TestsApp.Models.Users;

namespace TestsApp.ViewModels.FromServer
{
    public abstract class UserViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Fullname { get; set; }
        
        public UserViewModel()
        {
        }
    }
}