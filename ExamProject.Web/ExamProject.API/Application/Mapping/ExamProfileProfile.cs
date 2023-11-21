using AutoMapper;
using ExamProject.API.Application.DTOs;
using ExamProject.API.Core.Entities;

namespace ExamProject.API.Application.Mapping;

public class ExamProfileProfile : Profile
{
    public ExamProfileProfile()
    {
        CreateMap<Exam, ExamDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

        CreateMap<Exam, ExamDto>().ReverseMap();

        CreateMap<Exam, GetExamDto>().ReverseMap();
    }
}
