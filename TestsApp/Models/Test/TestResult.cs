using System;
using System.Collections.Generic;
using System.Linq;
using TestsApp.Models.Test.Answers;
using TestsApp.Models.Test.Questions;
using TestsApp.Models.Users;

namespace TestsApp.Models.Test
{
    public class TestResult
    {
        public int Id { get; set; }

        public Test Test { get; set; }
        public int TestId { get; set; }

        public StudentUser Student { get; set; }
        public string StudentId { get; set; }

        /// <summary>
        /// Время начала прохождения теста.
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Время окончания прохождения теста, если null, тест еще не окончен.
        /// </summary>
        public DateTime? FinishTime { get; set; } = null;

        /// <summary>
        /// Результат в баллах. Вычисляется после окончания прохождения теста методом <see cref="FinishTest"/>.
        /// </summary>
        public int? Result { get; set; }

        public List<Answer> Answers { get; set; }

        /// <summary>
        /// Вычисляет результат теста.
        /// Answers, Test и Test.Questions должны быть загружены.
        /// Изменяет FinishTime на текущее время.
        /// </summary>
        public void FinishTest()
        {
            FinishTime = DateTime.Now;

            Result = 0;
            foreach (Question question in this.Test.Questions)
            {
                Answer answer = this.Answers.FirstOrDefault(a => a.QuestionNumber == question.Number);
                if (answer == null) continue;

                if (question is OneChoiceQuestion oneChoiceQuestion &&
                    answer is OneChoiceAnswer oneChoiceAnswer &&
                    oneChoiceQuestion.RightAnswerNum == oneChoiceAnswer.ChosenAnswerNum)
                    Result += question.Score;

//                if (question is MultiChoiceQuestion multiChoiceQuestion &&
//                    answer is MultiChoiceAnswer multiChoiceAnswer &&
//                    multiChoiceQuestion.SerializedRigthAnswersNums == multiChoiceAnswer.SerializedAnswersNums)
//                    Result+=question.Score;

                if (question is MultiChoiceQuestion multiChoiceQuestion &&
                    answer is MultiChoiceAnswer multiChoiceAnswer &&
                    multiChoiceQuestion.RightAnswersNums.OrderBy(i => i)
                        .SequenceEqual(multiChoiceAnswer.AnswersNums.OrderBy(i => i)))
                    Result += question.Score;

//                if (question is TwoColumnsQuestion twoColumnsQuestion &&
//                    answer is TwoColumnsAnswer twoColumnsAnswer &&
//                    twoColumnsQuestion.SerializedConnections == twoColumnsAnswer.SerializedConnections)
//                    Result++;

                if (question is TwoColumnsQuestion twoColumnsQuestion &&
                    answer is TwoColumnsAnswer twoColumnsAnswer &&
                    twoColumnsQuestion.Connections.OrderBy(c => c)
                        .SequenceEqual(twoColumnsAnswer.Connections.OrderBy(c => c)))
                    Result += question.Score;

                if (question is OpenAnswerQuestion openAnswerQuestion &&
                    answer is OpenAnswerAnswer openAnswerAnswer &&
                    openAnswerQuestion.RightAnswer == openAnswerAnswer.Answer)
                    Result += question.Score;
            }
        }
    }
}
