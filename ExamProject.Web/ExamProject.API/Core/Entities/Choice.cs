namespace ExamProject.API.Core.Entities;

public class Choice : BaseEntity
{
    public string ChoiceText { get; set; } = null!;
    public bool IsCorrect { get; set; }

    //Nav. Prop
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}
