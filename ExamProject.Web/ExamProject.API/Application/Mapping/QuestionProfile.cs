using AutoMapper;
using ExamProject.API.Application.DTOs;
using ExamProject.API.Core.Entities;

namespace ExamProject.API.Application.Mapping;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<Question, QuestionDto>()
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.QuestionContent))
            .ForMember(dest => dest.Option1, opt => opt.MapFrom(src => src.Choices.FirstOrDefault().ChoiceText))
            .ForMember(dest => dest.Option2, opt => opt.MapFrom(src => src.Choices.Skip(1).FirstOrDefault().ChoiceText))
            .ForMember(dest => dest.Option3, opt => opt.MapFrom(src => src.Choices.Skip(2).FirstOrDefault().ChoiceText))
            .ForMember(dest => dest.Option4, opt => opt.MapFrom(src => src.Choices.Skip(3).FirstOrDefault().ChoiceText))
            .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => src.Choices.FirstOrDefault(c => c.IsCorrect).ChoiceText));
    }
}
