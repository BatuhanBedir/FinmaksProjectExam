using ExamProject.API.Core.Entities;
using ExamProject.API.Core.Interfaces;
using ExamProject.API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.API.Infrastructure.Repositories;

public class ExamRepository : Repository<Exam>, IExamRepository
{
    public ExamRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Exam>> GetByIdExamIncludeQuestionAndChoiceAsync(int id)
    {
        return await _context.Exams.Where(x => x.Id == id).Include(x => x.Questions).ThenInclude(x => x.Choices).ToListAsync() ;
    }
}
