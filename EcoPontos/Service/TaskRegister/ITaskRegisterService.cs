using EcoPontos.Service.TaskRegister.Dto;

namespace EcoPontos.Service.TaskRegister;

public interface ITaskRegisterService
{
    Task<TaskAssignDto> AssignTaskToUserAsync(int userId, int taskId);
    Task<IEnumerable<GetTasksDto>> GetTasksByUserIdAsync(int userId);
    Task<bool> UnassignTaskFromUserAsync(int userId, int taskId);
    Task<int> CompleteTaskAsync(int userId, int taskId, int durationInSeconds);
}
