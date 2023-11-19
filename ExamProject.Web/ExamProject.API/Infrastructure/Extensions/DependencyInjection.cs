using ExamProject.API.Application.Services;
using ExamProject.API.Application.Settings;
using ExamProject.API.Core.Interfaces;
using ExamProject.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.API.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IChoiceRepository, ChoiceRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();

        services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));
        
        return services;
    }
}
