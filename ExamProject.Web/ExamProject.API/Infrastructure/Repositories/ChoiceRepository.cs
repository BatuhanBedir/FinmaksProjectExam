using ExamProject.API.Core.Entities;
using ExamProject.API.Core.Interfaces;
using ExamProject.API.Infrastructure.Context;

namespace ExamProject.API.Infrastructure.Repositories;

public class ChoiceRepository : Repository<Question>, IChoiceRepository
{
    public ChoiceRepository(AppDbContext context) : base(context)
    {
    }
}
