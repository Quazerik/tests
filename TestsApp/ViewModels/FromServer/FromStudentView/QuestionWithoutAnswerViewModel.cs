using Microsoft.AspNetCore.Mvc;
using TestsApp.Enums;
using TestsApp.JsonUtils;
using TestsApp.Models.Test.Json;
using TestsApp.ViewModels.Both.AnswerVariants;

namespace TestsApp.ViewModels.Both.Question
{
    public class QuestionWithoutAnswerViewModel
    {
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

        public AnswerVariantViewModel[] AnswerVariants { get; set; }

        public AnswerVariantViewModel[] LeftColumn { get; set; }

        public AnswerVariantViewModel[] RightColumn { get; set; }

        public QuestionType QuestionType { get; set; }

        public QuestionWithoutAnswerViewModel() { }
    }
}
