using EcoPontos.Infrastructure.Repositories.Interface;
using EcoPontos.Service.TaskWork.Dto;

namespace EcoPontos.Service.TaskWork;

public class TaskWorkService : ITaskWorkService
{
    private readonly ITaskWorkRepository _taskWorkRepository;

    public TaskWorkService(ITaskWorkRepository taskWorkRepository)
    {
        _taskWorkRepository = taskWorkRepository;
    }

    public async Task<Domain.TaskWork.TaskWork> CreateTaskAsync(CreateTaskWorkDto dto)
    {
        var taskWork = new Domain.TaskWork.TaskWork
        {
            Type = dto.Type,
            Description = dto.Description
        };

        return await _taskWorkRepository.CreateAsync(taskWork);
    }

    public async Task<IEnumerable<Domain.TaskWork.TaskWork>> GetTaskAllAsync()
    {
        return await _taskWorkRepository.GetAllAsync();
    }

    public async Task<Domain.TaskWork.TaskWork?> GetTaskByIdAsync(int id)
    {
        return await _taskWorkRepository.GetByIdAsync(id);
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var taskWork = await _taskWorkRepository.GetByIdAsync(id);
        if (taskWork == null)
        {
            return false;
        }

        await _taskWorkRepository.DeleteAsync(taskWork);
        return true;
    }
}
