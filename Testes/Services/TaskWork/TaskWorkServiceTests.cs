using EcoPontos.Infrastructure.Repositories.Interface;
using EcoPontos.Service.TaskWork;
using EcoPontos.Service.TaskWork.Dto;
using Moq;

namespace Testes.Services.TaskWork;

public class TaskWorkServiceTests
{
    private readonly Mock<ITaskWorkRepository> _mockTaskWorkRepository;
    private readonly TaskWorkService _taskWorkService;

    public TaskWorkServiceTests()
    {
        _mockTaskWorkRepository = new Mock<ITaskWorkRepository>();
        _taskWorkService = new TaskWorkService(_mockTaskWorkRepository.Object);
    }

    [Fact]
    public async Task CreateTaskAsync_ShouldReturnCreatedTask_WhenTaskIsCreated()
    {
        // Arrange
        var dto = new CreateTaskWorkDto { Type = "Type1", Description = "Task description" };
        var taskWork = new EcoPontos.Domain.TaskWork.TaskWork { Type = dto.Type, Description = dto.Description };

        _mockTaskWorkRepository.Setup(repo => repo.CreateAsync(It.IsAny<EcoPontos.Domain.TaskWork.TaskWork>()))
            .ReturnsAsync(taskWork);

        // Act
        var result = await _taskWorkService.CreateTaskAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.Type, result.Type);
        Assert.Equal(dto.Description, result.Description);
    }

    [Fact]
    public async Task GetTaskAllAsync_ShouldReturnAllTasks_WhenTasksExist()
    {
        // Arrange
        var taskList = new List<EcoPontos.Domain.TaskWork.TaskWork>
        {
            new EcoPontos.Domain.TaskWork.TaskWork { Type = "Type1", Description = "Description1" },
            new EcoPontos.Domain.TaskWork.TaskWork { Type = "Type2", Description = "Description2" }
        };

        _mockTaskWorkRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(taskList);

        // Act
        var result = await _taskWorkService.GetTaskAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskList.Count, result.Count());
    }

    [Fact]
    public async Task GetTaskByIdAsync_ShouldReturnTask_WhenTaskExists()
    {
        // Arrange
        int taskId = 1;
        var task = new EcoPontos.Domain.TaskWork.TaskWork { TaskWorkId = taskId, Type = "Type1", Description = "Task description" };

        _mockTaskWorkRepository.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(task);

        // Act
        var result = await _taskWorkService.GetTaskByIdAsync(taskId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskId, result.TaskWorkId);
    }

    [Fact]
    public async Task DeleteTaskAsync_ShouldReturnTrue_WhenTaskIsDeleted()
    {
        // Arrange
        int taskId = 1;
        var task = new EcoPontos.Domain.TaskWork.TaskWork { TaskWorkId = taskId, Type = "Type1", Description = "Task description" };

        _mockTaskWorkRepository.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(task);
        _mockTaskWorkRepository.Setup(repo => repo.DeleteAsync(task)).Returns(Task.CompletedTask);

        // Act
        var result = await _taskWorkService.DeleteTaskAsync(taskId);

        // Assert
        Assert.True(result);
    }
}