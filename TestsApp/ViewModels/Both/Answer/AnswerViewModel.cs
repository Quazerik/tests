using TestsApp.Enums;

namespace TestsApp.ViewModels.Both.Answer
{
    public class AnswerViewModel
    {
        public int QuestionNumber { get; set; }
        public int[] AnswersNums { get; set; }
        public int ChosenAnswerNum { get; set; }
        public string Answer { get; set; }
        public (int, int)[] Connections { get; set; }
        public AnswerType AnswerType { get; set; }

        public AnswerViewModel(){}
    }
}
