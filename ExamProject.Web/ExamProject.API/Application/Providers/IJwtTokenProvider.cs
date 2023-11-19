using ExamProject.API.Core.Entities;

namespace ExamProject.API.Application.Providers;

public interface IJwtTokenProvider
{
    Task<string> GenerateToken(string userName, string role);
}
