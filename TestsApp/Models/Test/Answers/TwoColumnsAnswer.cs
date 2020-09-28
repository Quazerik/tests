using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TestsApp.Models.Test.Answers
{
    public class TwoColumnsAnswer : Answer
    {
        [NotMapped]
        public (int, int)[] Connections
        {
            get { return JsonConvert.DeserializeObject<(int, int)[]>(SerializedConnections); }
            set { SerializedConnections = JsonConvert.SerializeObject(value); }
        }

        public string SerializedConnections { get; set; }
    }
}
