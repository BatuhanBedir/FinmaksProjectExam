namespace ExamProject.API.Application.Settings;

public class JwtOptions
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Key { get; set; } = null!;
    public double DurationInMinutes { get; set; }
}
