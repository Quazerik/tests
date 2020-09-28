using System;
using System.Collections.Generic;
using TestsApp.Models.Test;
using TestsApp.Models.Test.Answers;
using TestsApp.ViewModels.Both.Answer;

namespace TestsApp.ViewModels.FromServer
{
    public class TestResultViewModel
    {
        public string TestName { get; set; }
        public int Id { get; set; }
        public int TestId { get; set; }

        public StudentUserViewModel Student { get; set; }

        public string StudentId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? FinishTime { get; set; }

        public int? Result { get; set; }

        public List<AnswerViewModel> Answers { get; set; }

        public TestResultViewModel()
        {
        }
    }
}