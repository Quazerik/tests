using TestsApp.Models.Users;

namespace TestsApp.ViewModels.FromServer
{
    public class StudentUserViewModel : UserViewModel
    {
        public int GroupId { get; set; }

        public StudentUserViewModel() : base()
        {
        }
    }
}