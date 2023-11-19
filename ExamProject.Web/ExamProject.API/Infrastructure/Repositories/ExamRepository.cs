using ExamProject.API.Core.Entities;
using ExamProject.API.Core.Interfaces;
using ExamProject.API.Infrastructure.Context;

namespace ExamProject.API.Infrastructure.Repositories;

public class ExamRepository : Repository<Exam>, IExamRepository
{
    public ExamRepository(AppDbContext context) : base(context)
    {
    }
}
