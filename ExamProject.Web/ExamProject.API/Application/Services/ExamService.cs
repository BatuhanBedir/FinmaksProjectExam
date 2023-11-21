using AutoMapper;
using ExamProject.API.Application.DTOs;
using ExamProject.API.Application.IInterfaces;
using ExamProject.API.Core.Entities;
using ExamProject.API.Core.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Net;

namespace ExamProject.API.Application.Services;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ExamService(IExamRepository examRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _examRepository = examRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<ExamDto>> CreateExam(ExamDto examDto)
    {
        try
        {
            var exam = new Exam
            {
                Title = examDto.Title,
                Content = examDto.Content,
                Questions = new List<Question>()
            };

            foreach (var questionDto in examDto.Questions)
            {
                var question = new Question
                {
                    QuestionContent = questionDto.Question,
                    Choices = new List<Choice>()
                };

                foreach (var choiceDto in new[] { questionDto.Option1, questionDto.Option2, questionDto.Option3, questionDto.Option4 })
                {
                    var choice = new Choice
                    {
                        ChoiceText = choiceDto,
                        IsCorrect = choiceDto == questionDto.CorrectAnswer
                    };

                    question.Choices.Add(choice);
                }

                exam.Questions.Add(question);
            }

            await _examRepository.AddAsync(exam);
            await _unitOfWork.SaveAsync();

            return CustomResponseDto<ExamDto>.Success(HttpStatusCode.Created.GetHashCode());
        }
        catch (Exception ex)
        {
            return CustomResponseDto<ExamDto>.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError.GetHashCode());
        }
    }

    public async Task<CustomResponseDto<List<GetExamDto>>> GetAllExam()
    {
        try
        {
            var exam = await _examRepository.GetAllAsync();

            var examDtos = _mapper.Map<List<GetExamDto>>(exam);

            return CustomResponseDto<List<GetExamDto>>.Success(examDtos, HttpStatusCode.OK.GetHashCode());
        }
        catch (Exception ex)
        {
            return CustomResponseDto<List<GetExamDto>>.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError.GetHashCode());
        }


    }

    public async Task<CustomResponseDto<bool>> DeleteExam(int id)
    {
        try
        {
            var exam = await _examRepository.GetByIdAsync(id);

            if (exam is null) return CustomResponseDto<bool>.Fail("exam not found", HttpStatusCode.NotFound.GetHashCode());

            _examRepository.Delete(exam);
            await _unitOfWork.SaveAsync();

            return CustomResponseDto<bool>.Success(HttpStatusCode.OK.GetHashCode());
        }
        catch (Exception ex)
        {

            return CustomResponseDto<bool>.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError.GetHashCode());
        }
    }

    public async Task<CustomResponseDto<ExamDto>> GetByIdExamIncludeQuestionAndChoiceAsync(int id)
    {
        try
        {
            var exams = await _examRepository.GetByIdExamIncludeQuestionAndChoiceAsync(id);

            if (exams is null) return CustomResponseDto<ExamDto>.Fail("not found", HttpStatusCode.NotFound.GetHashCode());

            var examDto = _mapper.Map<ExamDto>(exams);

            return CustomResponseDto<ExamDto>.Success(examDto, HttpStatusCode.OK.GetHashCode());
        }
        catch (Exception ex)
        {

            return CustomResponseDto<ExamDto>.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError.GetHashCode());
        }
    }
}
