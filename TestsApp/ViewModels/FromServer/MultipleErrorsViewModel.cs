using System.Collections.Generic;

namespace TestsApp.ViewModels.FromServer
{
    public class MultipleErrorsViewModel
    {
        public List<string> Messages { get; set; }

        public MultipleErrorsViewModel(List<string> messages)
        {
            Messages = messages;
        }
    }
}
