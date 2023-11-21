namespace ExamProject.Web.Models.ViewModels;

public record ExamListVm
{
    public List<Exams> Exams { get; set; }
    public string Role { get; set; }
}

public record Exams
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
}
