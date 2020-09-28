using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TestsApp.Models.Test.Answers
{
    public class MultiChoiceAnswer : Answer
    {
        [NotMapped]
        public int[] AnswersNums
        {
            get { return JsonConvert.DeserializeObject<int[]>(SerializedAnswersNums); }
            set { SerializedAnswersNums = JsonConvert.SerializeObject(value); }
        }

        public string SerializedAnswersNums { get; set; }
    }
}
