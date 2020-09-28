using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TestsApp.Models.Test.Questions
{
    public class MultiChoiceQuestion : QuestionWithAnswerVariants
    {
        [NotMapped]
        public int[] RightAnswersNums
        {
            get { return JsonConvert.DeserializeObject<int[]>(SerializedRightAnswersNums); }
            set { SerializedRightAnswersNums = JsonConvert.SerializeObject(value); }
        }

        public string SerializedRightAnswersNums { get; set; }
    }
}
