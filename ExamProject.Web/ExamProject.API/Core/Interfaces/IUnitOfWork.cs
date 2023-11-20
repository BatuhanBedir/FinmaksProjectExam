namespace ExamProject.API.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task SaveAsync();
}
