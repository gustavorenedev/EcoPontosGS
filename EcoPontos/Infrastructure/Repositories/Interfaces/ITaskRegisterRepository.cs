using EcoPontos.Domain.TaskRegister;

namespace EcoPontos.Infrastructure.Repositories.Interface;

public interface ITaskRegisterRepository
{
    Task<IEnumerable<TaskRegister>> GetTasksByUserIdAsync(int userId);
    Task<TaskRegister> AddAsync(TaskRegister taskRegister);
    Task<TaskRegister?> GetTaskRegisterAsync(int userId, int taskId);
    Task RemoveAsync(TaskRegister taskRegister);
    Task UpdateAsync(TaskRegister taskRegister);
}
