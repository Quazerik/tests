using System.Collections.Generic;

namespace TestsApp.Models.Users
{
    public class TeacherUser : AppUser
    {
        public List<Group> Groups { get; set; }
    }
}
