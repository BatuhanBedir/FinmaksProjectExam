using ExamProject.API.Application.DTOs;
using ExamProject.API.Application.IInterfaces;
using ExamProject.API.Core.Entities;
using ExamProject.API.Core.Interfaces;
using System.Net;

namespace ExamProject.API.Application.Services;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ExamService(IExamRepository examRepository, IUnitOfWork unitOfWork)
    {
        _examRepository = examRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponseDto<ExamDto>> CreateExam(ExamDto examDto)
    {
        try
        {
            var exam = new Exam
            {
                Name = examDto.Title,
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

            return CustomResponseDto<ExamDto>.Success(HttpStatusCode.OK.GetHashCode());
        }
        catch (Exception ex)
        {
            return CustomResponseDto<ExamDto>.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError.GetHashCode());
        }
    }
}
