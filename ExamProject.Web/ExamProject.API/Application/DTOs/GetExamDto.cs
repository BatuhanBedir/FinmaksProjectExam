namespace ExamProject.API.Application.DTOs;

public record GetExamDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
}
