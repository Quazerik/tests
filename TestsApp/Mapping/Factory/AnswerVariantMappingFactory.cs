using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestsApp.Enums;
using TestsApp.Models.Test.Json;
using TestsApp.Models.Test.Questions;
using TestsApp.ViewModels.Both.AnswerVariants;
using TestsApp.ViewModels.Both.Question;

namespace TestsApp.Mapping
{
    public class AnswerVariantMappingFactory : IMappingFactory<AnswerVariantType, AnswerVariantViewModel, AnswerVariant>
    {
        public Dictionary<AnswerVariantType, Func<AnswerVariantViewModel, AnswerVariant>> _answerVariantTypeMapper;

        public AnswerVariantMappingFactory()
        {
            _answerVariantTypeMapper = new Dictionary<AnswerVariantType, Func<AnswerVariantViewModel, AnswerVariant>>();
            _answerVariantTypeMapper.Add(AnswerVariantType.ImageAnswerVariant, viewModel => Mapper.Map<ImageAnswerVariant>(viewModel));
            _answerVariantTypeMapper.Add(AnswerVariantType.TextAnswerVariant, viewModel => Mapper.Map<TextAnswerVariant>(viewModel));
        }

        public AnswerVariant Map(AnswerVariantViewModel viewModel)
        {
            return _answerVariantTypeMapper[viewModel.AnswerVariantType](viewModel);
        }
    }
}
