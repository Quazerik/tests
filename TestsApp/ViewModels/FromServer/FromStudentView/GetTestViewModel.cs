using System.Collections.Generic;
using TestsApp.ViewModels.Both.Question;

namespace TestsApp.ViewModels.FromServer
{
    public class GetTestViewModel
    {
        public int TestId { get; set; }
        public int GroupId { get; set; }

        public string Name { get; set; }

        public List<QuestionWithoutAnswerViewModel> Questions { get; set; }

        public int RemainingSeconds { get; set; }

        public GetTestViewModel()
        {
        }
    }
}
