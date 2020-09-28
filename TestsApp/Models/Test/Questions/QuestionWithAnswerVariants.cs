using System.ComponentModel.DataAnnotations.Schema;
using TestsApp.JsonUtils;
using TestsApp.Models.Test.Json;

namespace TestsApp.Models.Test.Questions
{
    public abstract class QuestionWithAnswerVariants : Question
    {
        [NotMapped]
        public AnswerVariant[] AnswerVariants
        {
            get { return MyJsonConvert.DeserializeObject<AnswerVariant[]>(SerializedAnswerVariants); }
            set { SerializedAnswerVariants = MyJsonConvert.SerializeObject(value); }
        }

        public string SerializedAnswerVariants { get; set; }

        public QuestionWithAnswerVariants()
        {
        }
    }
}
