using AutoMapper;
using EcoPontos.Infrastructure.Repositories.Interface;
using EcoPontos.Service.TaskRegister.Dto;
using EcoPontos.Service.User.Dto;

namespace EcoPontos.Service.TaskRegister;

public class TaskRegisterService : ITaskRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly ITaskWorkRepository _taskWorkRepository;
    private readonly ITaskRegisterRepository _taskRegisterRepository;
    private readonly IMapper _mapper;

    public TaskRegisterService(IUserRepository userRepository, ITaskRegisterRepository taskRegisterRepository, ITaskWorkRepository taskWorkRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _taskRegisterRepository = taskRegisterRepository;
        _taskWorkRepository = taskWorkRepository;
        _mapper = mapper;
    }

    public async Task<TaskAssignDto> AssignTaskToUserAsync(int userId, int taskId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        var task = await _taskWorkRepository.GetByIdAsync(taskId);

        if (user == null || task == null)
            throw new Exception("User or Task not found");

        if (user.TaskRegisters == null)
            user.TaskRegisters = new List<Domain.TaskRegister.TaskRegister>();

        var existingTaskRegister = user.TaskRegisters
                                       .FirstOrDefault(tr => tr.TaskWorkId == taskId);
        if (existingTaskRegister != null)
            throw new Exception("This task is already assigned to the user.");

        var newTaskRegister = new Domain.TaskRegister.TaskRegister
        {
            User = user,
            Task = task,
            TaskDateCompleted = DateTime.Now,
            Duration = 0,
            PointsTotal = 0
        };

        user.TaskRegisters.Add(newTaskRegister);

        await _userRepository.UpdateUserAsync(user);

        var taskAssign = _mapper.Map<TaskAssignDto>(newTaskRegister);

        return taskAssign;
    }


    public async Task<IEnumerable<GetTasksDto>> GetTasksByUserIdAsync(int userId)
    {
        var result = await _taskRegisterRepository.GetTasksByUserIdAsync(userId);

        var groupedTasks = result
            .GroupBy(tr => tr.User)
            .Select(group => new GetTasksDto
            {
                User = new InfoUserReturn
                {
                    Name = group.Key!.Name,
                    Email = group.Key.Email
                },
                TaskWorks = group.Select(tr => tr.Task).ToList()!
            })
            .ToList();

        return groupedTasks;
    }

    public async Task<bool> UnassignTaskFromUserAsync(int userId, int taskId)
    {
        var taskRegister = await _taskRegisterRepository.GetTaskRegisterAsync(userId, taskId);

        if (taskRegister == null)
            return false;

        await _taskRegisterRepository.RemoveAsync(taskRegister);

        return true;
    }

    public async Task<int> CompleteTaskAsync(int userId, int taskId, int durationInSeconds)
    {
        var taskRegister = await _taskRegisterRepository.GetTaskRegisterAsync(userId, taskId);

        if (taskRegister == null)
            throw new Exception("Task Register not found");

        taskRegister.Duration = durationInSeconds;
        taskRegister.TaskDateCompleted = DateTime.Now;

        await _taskRegisterRepository.UpdateAsync(taskRegister);

        var updatedPoints = await _userRepository.UpdateScoreAsync(userId, durationInSeconds);

        await UnassignTaskFromUserAsync(userId, taskId);

        return updatedPoints;
    }
}
