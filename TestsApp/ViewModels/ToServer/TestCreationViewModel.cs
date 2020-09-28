using System;
using System.Collections.Generic;
using TestsApp.Models.Test.Questions;
using TestsApp.ViewModels.Both.Question;

namespace TestsApp.ViewModels.ToServer
{
    public class TestCreationViewModel
    {
        // TODO

        public string Name { get; set; }
        
        public int TimeInSeconds { get; set; }
        
        public List<QuestionViewModel> Questions { get; set; }
        
//        public List<MultiChoiceQuestionViewModel> MultiChoiceQuestions { get; set; }
//        
//        public List<OneChoiceQuestionViewModel> OneChoiceQuestions { get; set; }
//        
//        public List<OpenAnswerQuestionViewModel> OpenAnswerQuestions { get; set; }
//        
//        public List<TwoColumnsQuestionViewModel> TwoColumnsQuestions { get; set; }
        public TestCreationViewModel()
        {
        }
    }
}
