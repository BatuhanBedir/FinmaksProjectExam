namespace ExamProject.API.Application.DTOs;

public record AppUserLoginDto
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
