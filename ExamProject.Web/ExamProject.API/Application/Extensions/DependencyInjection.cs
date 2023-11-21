using ExamProject.API.Application.IInterfaces;
using ExamProject.API.Application.Providers;
using ExamProject.API.Application.Services;
using ExamProject.API.Application.Settings;
using ExamProject.API.Core.Interfaces;
using ExamProject.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ExamProject.API.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));


        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

        services.AddScoped<IExamService, ExamService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        services.AddScoped<IRssService, RssService>();
        services.AddAutoMapper(typeof(DependencyInjection));

        return services;

    }
}
