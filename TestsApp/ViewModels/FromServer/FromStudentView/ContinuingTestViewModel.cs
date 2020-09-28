using System;

namespace TestsApp.ViewModels.FromServer
{
    public class ContinuingTestViewModel
    {
        public int TestId { get; set; }
        public int GroupId { get; set; }

        public string Name { get; set; }

        public int RemainingTime { get; set; }
    }
}
