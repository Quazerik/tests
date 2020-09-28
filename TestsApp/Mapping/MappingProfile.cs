using System;
using System.Linq;
using AutoMapper;
using TestsApp.Enums;
using TestsApp.Models;
using TestsApp.Models.Test;
using TestsApp.Models.Test.Answers;
using TestsApp.Models.Test.Json;
using TestsApp.Models.Test.Questions;
using TestsApp.Models.Users;
using TestsApp.ViewModels.Both.Answer;
using TestsApp.ViewModels.Both.AnswerVariants;
using TestsApp.ViewModels.Both.Question;
using TestsApp.ViewModels.FromServer;
using TestsApp.ViewModels.ToServer;

namespace TestsApp.Mapping
{
    public class MappingProfile : Profile
    {
        private readonly AnswerVariantMappingFactory variantMappingFactory = new AnswerVariantMappingFactory();
        private readonly QuestionMappingFactory questionMappingFactory = new QuestionMappingFactory();
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<AppUser, LoginViewModel>()
                .ForMember(dest => dest.Role, opts => opts.MapFrom(
                    src => (Role)Enum.Parse(typeof(Role), src.GetType().Name))
                );
            CreateMap<AppUser, LoginViewModel>()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(
                    src => src.FullName ?? "")
                );

            CreateMap<AppUser, UserViewModel>().IncludeAllDerived();
            CreateMap<StudentUser, StudentUserViewModel>();
            CreateMap<TeacherUser, TeacherUserViewModel>();
            CreateMap<Group, GroupViewModel>();

            CreateMap<MultiChoiceAnswer, AnswerViewModel>()
                .ForMember(dest => dest.AnswerType, opts => opts.MapFrom(
                    src => AnswerType.MultiChoice)
                );
            CreateMap<OneChoiceAnswer, AnswerViewModel>()
                .ForMember(dest => dest.AnswerType, opts => opts.MapFrom(
                    src => AnswerType.OneChoice)
                );
            CreateMap<OpenAnswerAnswer, AnswerViewModel>()
                .ForMember(dest => dest.AnswerType, opts => opts.MapFrom(
                    src => AnswerType.OpenAnswer)
                );
            CreateMap<TwoColumnsAnswer, AnswerViewModel>()
                .ForMember(dest => dest.AnswerType, opts => opts.MapFrom(
                    src => AnswerType.TwoColumns)
                );

            CreateMap<AnswerViewModel, MultiChoiceAnswer>();
            CreateMap<AnswerViewModel, OneChoiceAnswer>();
            CreateMap<AnswerViewModel, OpenAnswerAnswer>();
            CreateMap<AnswerViewModel, TwoColumnsAnswer>();


            CreateMap<Question, QuestionViewModel>();
            //CreateMap<QuestionWithAnswerVariants, QuestionViewModel>()
            //    .ForMember(dest => dest.QuestionType, opts => opts.MapFrom(
            //        src => QuestionType.AnswerVariants)
            //    );
            CreateMap<MultiChoiceQuestion, QuestionViewModel>()
                .ForMember(dest => dest.QuestionType, opts => opts.MapFrom(
                src => QuestionType.MultiChoice)
            );
            CreateMap<OneChoiceQuestion, QuestionViewModel>()
                .ForMember(dest => dest.QuestionType, opts => opts.MapFrom(
                src => QuestionType.OneChoice)
            );
            CreateMap<OpenAnswerQuestion, QuestionViewModel>()
                .ForMember(dest => dest.QuestionType, opts => opts.MapFrom(
                    src => QuestionType.OpenAnswer)
                );
            CreateMap<TwoColumnsQuestion, QuestionViewModel>()
                .ForMember(dest => dest.QuestionType, opts => opts.MapFrom(
                    src => QuestionType.TwoColumns)
                );

            CreateMap<QuestionViewModel, QuestionWithAnswerVariants>()
                .ForMember(dest => dest.AnswerVariants, opts => opts.MapFrom(
                    src => src.AnswerVariants.Select(x => variantMappingFactory.Map(x)).ToList())
                ).IncludeAllDerived();
            CreateMap<QuestionViewModel, MultiChoiceQuestion>();
            CreateMap<QuestionViewModel, OneChoiceQuestion>();
            CreateMap<QuestionViewModel, OpenAnswerQuestion>();
            CreateMap<QuestionViewModel, TwoColumnsQuestion>()
                .ForMember(dest => dest.LeftColumn, opts => opts.MapFrom(
                    src => src.LeftColumn.Select(x => variantMappingFactory.Map(x)).ToList()))
                .ForMember(dest => dest.RightColumn, opts => opts.MapFrom(
                    src => src.RightColumn.Select(x => variantMappingFactory.Map(x)).ToList())
                );

            CreateMap<Test, TestExViewModel>()
                .ForMember(dest => dest.Questions, opts => opts.MapFrom(
                    src => src.Questions.Select(Mapper.Map<QuestionViewModel>).ToList())
                );

            #region QuestionWithoutAnswerViewModel
            CreateMap<Question, QuestionWithoutAnswerViewModel>();
            //CreateMap<QuestionWithAnswerVariants, QuestionWithoutAnswerViewModel>()
            //    .ForMember(dest => dest.QuestionType, opts => opts.MapFrom(
            //        src => QuestionType.AnswerVariants)
            //    );
            CreateMap<MultiChoiceQuestion, QuestionWithoutAnswerViewModel>()
                .ForMember(dest => dest.QuestionType, opts => opts.MapFrom(
                src => QuestionType.MultiChoice)
            );
            CreateMap<OneChoiceQuestion, QuestionWithoutAnswerViewModel>()
                .ForMember(dest => dest.QuestionType, opts => opts.MapFrom(
                src => QuestionType.OneChoice)
            );
            CreateMap<OpenAnswerQuestion, QuestionWithoutAnswerViewModel>()
                .ForMember(dest => dest.QuestionType, opts => opts.MapFrom(
                    src => QuestionType.OpenAnswer)
                );
            CreateMap<TwoColumnsQuestion, QuestionWithoutAnswerViewModel>()
                .ForMember(dest => dest.QuestionType, opts => opts.MapFrom(
                    src => QuestionType.TwoColumns)
                );

            CreateMap<QuestionWithoutAnswerViewModel, MultiChoiceQuestion>();
            CreateMap<QuestionWithoutAnswerViewModel, OneChoiceQuestion>();
            CreateMap<QuestionWithoutAnswerViewModel, OpenAnswerQuestion>();
            CreateMap<QuestionWithoutAnswerViewModel, TwoColumnsQuestion>();
            CreateMap<QuestionWithoutAnswerViewModel, QuestionWithAnswerVariants>()
                .ForMember(dest => dest.AnswerVariants, opts => opts.MapFrom(
                    src => src.AnswerVariants.Select(x => variantMappingFactory.Map(x)).ToList())
                );

            CreateMap<Test, GetTestViewModel>()
                .ForMember(dest => dest.Questions, opts => opts.MapFrom(
                    src => src.Questions.Select(Mapper.Map<QuestionWithoutAnswerViewModel>).ToList())
                );

            CreateMap<TestResult, GetTestViewModel>()
                .ForMember(dest => dest.RemainingSeconds, opts => opts.MapFrom(
                    src => (src.Test.TimeInSeconds - (int)(DateTime.Now - src.StartTime).TotalSeconds)))
                .ForMember(dest => dest.Questions, opts => opts.MapFrom(
                    src => src.Test.Questions.Select(Mapper.Map<QuestionWithoutAnswerViewModel>).ToList()))
                    .ForMember(dest => dest.TestId, opts => opts.MapFrom(
                    src => (src.TestId)))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(
                    src => (src.Test.Name)))
                    .ForMember(dest => dest.GroupId, opts => opts.MapFrom(
                    src => (src.Test.GroupId)));
            #endregion

            CreateMap<Test, StartingTestViewModel>();

            CreateMap<TestResult, ContinuingTestViewModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(
                    src => src.Test.Name))
                .ForMember(dest => dest.GroupId, opts => opts.MapFrom(
                    src => src.Test.GroupId))
                .ForMember(dest => dest.RemainingTime, opts => opts.MapFrom(
                    src => (src.Test.TimeInSeconds - (int)(DateTime.Now - src.StartTime).TotalSeconds)));

            CreateMap<TestResult, TestResultViewModel>()
                .ForMember(dest => dest.TestName, opts => opts.MapFrom(
                    src => src.Test.Name));

            CreateMap<AnswerVariantViewModel, AnswerVariant>().IncludeAllDerived();
            CreateMap<AnswerVariantViewModel, ImageAnswerVariant>();
            CreateMap<AnswerVariantViewModel, TextAnswerVariant>();

            CreateMap<ImageAnswerVariant, AnswerVariantViewModel>()
                .ForMember(dest => dest.AnswerVariantType, opts => opts.MapFrom(
                    src => AnswerVariantType.ImageAnswerVariant)
                );
            CreateMap<TextAnswerVariant, AnswerVariantViewModel>()
                .ForMember(dest => dest.AnswerVariantType, opts => opts.MapFrom(
                    src => AnswerVariantType.TextAnswerVariant)
                );

            CreateMissingTypeMaps = true;
        }
    }
}
