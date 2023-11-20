using ExamProject.API.Application.DTOs;

namespace ExamProject.API.Application.IInterfaces;

public interface IRssService
{
    Task<CustomResponseDto<List<ArticleDto>>> GetWiredArticle();
}
