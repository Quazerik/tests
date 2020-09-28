using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestsApp.Enums;

namespace TestsApp.ViewModels.Both.AnswerVariants
{
    public class AnswerVariantViewModel
    {
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public AnswerVariantType AnswerVariantType { get; set; }
    }
}
