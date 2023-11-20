using ExamProject.API.Core.Entities;
using ExamProject.API.Core.Interfaces;
using ExamProject.API.Infrastructure.Context;

namespace ExamProject.API.Infrastructure.Repositories;

public class ChoiceRepository : Repository<Choice>, IChoiceRepository
{
    public ChoiceRepository(AppDbContext context) : base(context)
    {
    }
}
