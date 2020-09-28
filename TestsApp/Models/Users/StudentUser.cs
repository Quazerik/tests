namespace TestsApp.Models.Users
{
    public class StudentUser : AppUser
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
