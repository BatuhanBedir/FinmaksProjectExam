using ExamProject.API.Application.Settings;
using Microsoft.Extensions.Options;

namespace ExamProject.API.Application.Options;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string Section_Name = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(Section_Name).Bind(options);
    }
}
