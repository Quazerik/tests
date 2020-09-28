namespace TestsApp.Models.Test.Answers
{
    public abstract class Answer
    {
        public TestResult TestResult { get; set; }
        public int TestResultId { get; set; }

        /// <summary>
        /// Порядковый номер вопроса в тесте.
        /// </summary>
        public int QuestionNumber { get; set; }
    }
}
