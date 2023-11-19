using ExamProject.API.Core.Entities;
using ExamProject.API.Core.Interfaces;
using ExamProject.API.Infrastructure.Context;

namespace ExamProject.API.Infrastructure.Repositories;

public class QuestionRepository : Repository<Question> , IQuestionRepository
{
    public QuestionRepository(AppDbContext context) : base(context)
    {
    }
}
