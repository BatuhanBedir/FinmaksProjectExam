namespace ExamProject.API.Core.Entities;

public class Exam : BaseEntity
{
    public Exam()
    {
        Questions = new HashSet<Question>();
    }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;

    //Nav. Prop.
    public ICollection<Question> Questions { get; set; }
}
