using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestsApp.Enums;
using TestsApp.Models.Test.Questions;
using TestsApp.ViewModels.Both.Question;

namespace TestsApp.Mapping
{
    public class QuestionWithoutAnswerMappingFactory : IMappingFactory<QuestionType, QuestionWithoutAnswerViewModel, Question>
    {
        public Dictionary<QuestionType, Func<QuestionWithoutAnswerViewModel, Question>> _questionTypeMapper;

        public QuestionWithoutAnswerMappingFactory()
        {
            _questionTypeMapper = new Dictionary<QuestionType, Func<QuestionWithoutAnswerViewModel, Question>>();
            _questionTypeMapper.Add(QuestionType.OneChoice, viewModel => Mapper.Map<OneChoiceQuestion>(viewModel));
            _questionTypeMapper.Add(QuestionType.MultiChoice, viewModel => Mapper.Map<MultiChoiceQuestion>(viewModel));
            _questionTypeMapper.Add(QuestionType.OpenAnswer, viewModel => Mapper.Map<OpenAnswerQuestion>(viewModel));
            _questionTypeMapper.Add(QuestionType.TwoColumns, viewModel => Mapper.Map<TwoColumnsQuestion>(viewModel));
            //_questionTypeMapper.Add(QuestionType.AnswerVariants, viewModel =>
            //    Mapper.Map<QuestionWithAnswerVariants>(viewModel));
        }

        public Question Map(QuestionWithoutAnswerViewModel viewModel)
        {
            return _questionTypeMapper[viewModel.QuestionType](viewModel);
        }
    }
}
