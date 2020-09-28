using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestsApp.Models.Test.Questions;
using TestsApp.ViewModels.ToServer;

namespace TestsApp.Models.Test
{
    public class Test
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Group Group { get; set; }
        public int GroupId { get; set; }

        /// <summary>
        /// Время, отведенное на решение теста в секундах
        /// </summary>
        public int TimeInSeconds { get; set; }


        public List<Question> Questions { get; set; }


        public Test()
        {
        }

        public Test(Group group, TestCreationViewModel testCreationViewModel, IEnumerable<Question> questions)
        {
            Name = testCreationViewModel.Name;
            Group = group;
            GroupId = group.Id;
            TimeInSeconds = testCreationViewModel.TimeInSeconds;
            Questions = new List<Question>(questions);
        }
    }
}
