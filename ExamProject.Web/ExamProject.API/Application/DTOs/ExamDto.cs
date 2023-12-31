﻿namespace ExamProject.API.Application.DTOs;
public record QuestionDto
{
    public int Id { get; set; }
    public string Question { get; set; } = null!;
    public string Option1 { get; set; } = null!;
    public string Option2 { get; set; } = null!;
    public string Option3 { get; set; } = null!;
    public string Option4 { get; set; } = null!;
    public string CorrectAnswer { get; set; } = null!;
}
public record ExamDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public List<QuestionDto> Questions { get; set; }
}
