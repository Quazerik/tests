using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using TestsApp.JsonUtils;
using TestsApp.Models.Test.Json;

namespace TestsApp.Models.Test.Questions
{
    public class TwoColumnsQuestion : Question
    {
        [NotMapped]
        public AnswerVariant[] LeftColumn
        {
            get { return MyJsonConvert.DeserializeObject<AnswerVariant[]>(SerializedLeft); }
            set { SerializedLeft = MyJsonConvert.SerializeObject(value); }
        }

        public string SerializedLeft { get; set; }

        [NotMapped]
        public AnswerVariant[] RightColumn
        {
            get { return MyJsonConvert.DeserializeObject<AnswerVariant[]>(SerializedRight); }
            set { SerializedRight = MyJsonConvert.SerializeObject(value); }
        }

        public string SerializedRight { get; set; }


        [NotMapped]
        public (int, int)[] Connections
        {
            get { return JsonConvert.DeserializeObject<(int, int)[]>(SerializedConnections); }
            set { SerializedConnections = JsonConvert.SerializeObject(value); }
        }

        public string SerializedConnections { get; set; }

        public void RandomizeOrder()
        {
            var cachedRight = RightColumn;
            int max = cachedRight.Length;
            Random random = new Random();
            var numsMapper = Enumerable.Range(0, max).OrderBy(x => random.Next()).ToArray();
            RightColumn = numsMapper
                .Select(i => cachedRight[i])
                .ToArray();
            Connections = Connections
                .Select(c => (c.Item1, numsMapper.IndexOf(c.Item2)))
                .ToArray();
        }
    }
}
