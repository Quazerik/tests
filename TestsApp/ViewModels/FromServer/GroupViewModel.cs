using TestsApp.Models;

namespace TestsApp.ViewModels.FromServer
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherId { get; set; }

        public GroupViewModel()
        {
        }
    }
}