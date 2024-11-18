using EcoPontos.Domain.TaskWork;

namespace EcoPontos.Infrastructure.Repositories.Interface;

public interface ITaskWorkRepository
{
    Task<TaskWork> CreateAsync(TaskWork taskWork);
    Task<IEnumerable<TaskWork>> GetAllAsync();
    Task<TaskWork?> GetByIdAsync(int id);
    Task DeleteAsync(TaskWork taskWork);
}
