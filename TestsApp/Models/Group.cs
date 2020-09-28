using System.Collections.Generic;
using TestsApp.Models.Users;

namespace TestsApp.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StudentUser> Students { get; set; }

        public TeacherUser Teacher { get; set; }
        public string TeacherId { get; set; }
    }
}
