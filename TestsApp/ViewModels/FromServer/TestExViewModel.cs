using System.Collections.Generic;
using TestsApp.ViewModels.Both.Question;

namespace TestsApp.ViewModels.FromServer
{
    public class TestExViewModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }

        public string Name { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
        
        public int TimeInSeconds { get; set; }

        public TestExViewModel()
        {
        }
    }
}
