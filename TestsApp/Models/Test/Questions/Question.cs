using Microsoft.AspNetCore.Mvc;
using TestsApp.JsonUtils;

namespace TestsApp.Models.Test.Questions
{
    public abstract class Question
    {
        public Test Test { get; set; }
        public int TestId { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Порядковый номер вопроса (от 0)
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Баллы за правильный ответ
        /// </summary>
        public int Score { get; set; } = 1;
    }
}
