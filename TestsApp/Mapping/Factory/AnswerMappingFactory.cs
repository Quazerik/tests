using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestsApp.Enums;
using TestsApp.Models.Test.Answers;
using TestsApp.Models.Test.Questions;
using TestsApp.ViewModels.Both.Answer;
using TestsApp.ViewModels.Both.Question;

namespace TestsApp.Mapping
{
    public class AnswerMappingFactory : IMappingFactory<AnswerType, AnswerViewModel, Answer>
    {
        public Dictionary<AnswerType, Func<AnswerViewModel, Answer>> _answerTypeMapper;

        public AnswerMappingFactory()
        {
            _answerTypeMapper = new Dictionary<AnswerType, Func<AnswerViewModel, Answer>>();
            _answerTypeMapper.Add(AnswerType.OneChoice, viewModel => Mapper.Map<OneChoiceAnswer>(viewModel));
            _answerTypeMapper.Add(AnswerType.MultiChoice, viewModel => Mapper.Map<MultiChoiceAnswer>(viewModel));
            _answerTypeMapper.Add(AnswerType.OpenAnswer, viewModel => Mapper.Map<OpenAnswerAnswer>(viewModel));
            _answerTypeMapper.Add(AnswerType.TwoColumns, viewModel => Mapper.Map<TwoColumnsAnswer>(viewModel));
        }

        public Answer Map(AnswerViewModel viewModel)
        {
            return _answerTypeMapper[viewModel.AnswerType](viewModel);
        }
    }
}
