using ExamProject.API.Core.Entities;

namespace ExamProject.API.Core.Interfaces;

public interface IExamRepository : IRepository<Exam>
{
    public Task<List<Exam>> GetByIdExamIncludeQuestionAndChoiceAsync(int id);
}
