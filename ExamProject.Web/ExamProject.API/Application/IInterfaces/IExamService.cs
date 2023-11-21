using ExamProject.API.Application.DTOs;

namespace ExamProject.API.Application.IInterfaces;

public interface IExamService
{
    Task<CustomResponseDto<ExamDto>> CreateExam(ExamDto examDto);
    Task<CustomResponseDto<List<ExamDto>>> GetAllExamIncludeQuestionAndChoiceAsync();
}
