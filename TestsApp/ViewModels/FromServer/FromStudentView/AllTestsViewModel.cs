using System.Collections.Generic;

namespace TestsApp.ViewModels.FromServer
{
    public class AllTestsViewModel
    {
        public List<StartingTestViewModel> StartingTests { get; set; }
        public List<ContinuingTestViewModel> ContinuingTests { get; set; }
        public List<TestResultViewModel> FinishedTests { get; set; }
    }
}
