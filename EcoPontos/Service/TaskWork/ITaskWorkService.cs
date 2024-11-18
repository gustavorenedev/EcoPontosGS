using EcoPontos.Service.TaskWork.Dto;

namespace EcoPontos.Service.TaskWork;

public interface ITaskWorkService
{
    Task<Domain.TaskWork.TaskWork> CreateTaskAsync(CreateTaskWorkDto dto);
    Task<IEnumerable<Domain.TaskWork.TaskWork>> GetTaskAllAsync();
    Task<Domain.TaskWork.TaskWork?> GetTaskByIdAsync(int id);
    Task<bool> DeleteTaskAsync(int id);
}
