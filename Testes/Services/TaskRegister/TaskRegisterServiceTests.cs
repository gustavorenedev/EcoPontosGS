using AutoMapper;
using EcoPontos.Infrastructure.Repositories.Interface;
using EcoPontos.Service.TaskRegister;
using EcoPontos.Service.TaskRegister.Dto;
using EcoPontos.Service.User.Dto;
using Moq;

namespace Testes.Services.TaskRegister;

public class TaskRegisterServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ITaskRegisterRepository> _mockTaskRegisterRepository;
    private readonly Mock<ITaskWorkRepository> _mockTaskWorkRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly TaskRegisterService _taskRegisterService;

    public TaskRegisterServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockTaskRegisterRepository = new Mock<ITaskRegisterRepository>();
        _mockTaskWorkRepository = new Mock<ITaskWorkRepository>();
        _mockMapper = new Mock<IMapper>();
        _taskRegisterService = new TaskRegisterService(
            _mockUserRepository.Object,
            _mockTaskRegisterRepository.Object,
            _mockTaskWorkRepository.Object,
            _mockMapper.Object
        );
    }

    [Fact]
    public async Task AssignTaskToUserAsync_ShouldReturnTaskAssignDto_WhenTaskAssignedSuccessfully()
    {
        // Arrange
        int userId = 1, taskId = 1;
        var user = new EcoPontos.Domain.User.User { UserId = userId, TaskRegisters = new List<EcoPontos.Domain.TaskRegister.TaskRegister>() };
        var task = new EcoPontos.Domain.TaskWork.TaskWork { TaskWorkId = taskId };
        var taskAssignDto = new TaskAssignDto();

        _mockUserRepository.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);
        _mockTaskWorkRepository.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(task);
        _mockMapper.Setup(mapper => mapper.Map<TaskAssignDto>(It.IsAny<EcoPontos.Domain.TaskRegister.TaskRegister>()))
                   .Returns(taskAssignDto);

        // Act
        var result = await _taskRegisterService.AssignTaskToUserAsync(userId, taskId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskAssignDto, result);
    }

    [Fact]
    public async Task GetTasksByUserIdAsync_ShouldReturnListOfGetTasksDto_WhenTasksExist()
    {
        // Arrange
        int userId = 1;
        var taskRegisters = new List<EcoPontos.Domain.TaskRegister.TaskRegister>
        {
            new EcoPontos.Domain.TaskRegister.TaskRegister
            {
                User = new EcoPontos.Domain.User.User { UserId = userId, Name = "John Doe", Email = "john@example.com" },
                Task = new EcoPontos.Domain.TaskWork.TaskWork { Type = "Type1", Description = "Task 1" }
            }
        };

        var expectedResult = new List<GetTasksDto>
        {
            new GetTasksDto
            {
                User = new InfoUserReturn { Name = "John Doe", Email = "john@example.com" },
                TaskWorks = taskRegisters.Select(tr => tr.Task).ToList()!
            }
        };

        _mockTaskRegisterRepository.Setup(repo => repo.GetTasksByUserIdAsync(userId)).ReturnsAsync(taskRegisters);

        // Act
        var result = await _taskRegisterService.GetTasksByUserIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult.Count(), result.Count());
    }

    [Fact]
    public async Task UnassignTaskFromUserAsync_ShouldReturnTrue_WhenTaskUnassignedSuccessfully()
    {
        // Arrange
        int userId = 1, taskId = 1;
        var taskRegister = new EcoPontos.Domain.TaskRegister.TaskRegister { UserId = userId, TaskWorkId = taskId };

        _mockTaskRegisterRepository.Setup(repo => repo.GetTaskRegisterAsync(userId, taskId)).ReturnsAsync(taskRegister);
        _mockTaskRegisterRepository.Setup(repo => repo.RemoveAsync(taskRegister)).Returns(Task.CompletedTask);

        // Act
        var result = await _taskRegisterService.UnassignTaskFromUserAsync(userId, taskId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CompleteTaskAsync_ShouldReturnUpdatedPoints_WhenTaskCompletedSuccessfully()
    {
        // Arrange
        int userId = 1, taskId = 1, durationInSeconds = 3600;
        var taskRegister = new EcoPontos.Domain.TaskRegister.TaskRegister
        {
            UserId = userId,
            TaskWorkId = taskId,
            Duration = 0,
            TaskDateCompleted = DateTime.Now
        };

        _mockTaskRegisterRepository.Setup(repo => repo.GetTaskRegisterAsync(userId, taskId)).ReturnsAsync(taskRegister);
        _mockTaskRegisterRepository.Setup(repo => repo.UpdateAsync(taskRegister)).Returns(Task.CompletedTask);
        _mockUserRepository.Setup(repo => repo.UpdateScoreAsync(userId, durationInSeconds)).ReturnsAsync(100);
        _mockTaskRegisterRepository.Setup(repo => repo.RemoveAsync(taskRegister)).Returns(Task.CompletedTask);

        // Act
        var result = await _taskRegisterService.CompleteTaskAsync(userId, taskId, durationInSeconds);

        // Assert
        Assert.Equal(100, result);
    }
}