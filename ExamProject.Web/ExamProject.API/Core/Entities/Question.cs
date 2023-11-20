namespace ExamProject.API.Core.Entities;

public class Question : BaseEntity
{
    public Question()
    {
        Choices = new HashSet<Choice>();
    }

    public string QuestionContent { get; set; } = null!;

    //Nav.Prop.
    public int ExamId { get; set; }
    public Exam Exam { get; set; } = null!;
    public ICollection<Choice> Choices { get; set; }
}
