using AutoMapper;
using EcoPontos.Infrastructure.Repositories.Interface;
using EcoPontos.Service.Reward;
using EcoPontos.Service.Reward.Dtos;
using Moq;

namespace Testes.Services.Reward;

public class RewardServiceTests
{
    private readonly Mock<IRewardRepository> _mockRewardRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly RewardService _rewardService;

    public RewardServiceTests()
    {
        _mockRewardRepository = new Mock<IRewardRepository>();
        _mockMapper = new Mock<IMapper>();
        _rewardService = new RewardService(_mockRewardRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateRewardAsync_ShouldReturnCreatedRewardDto()
    {
        // Arrange
        var createDto = new CreateRewardDto { Description = "Test Reward", NecessaryPoints = 100 };
        var reward = new EcoPontos.Domain.Reward.Reward { RewardId = 1, Description = "Test Reward", NecessaryPoints = 100 };
        var expectedDto = new GetRewardDto { RewardId = 1, Description = "Test Reward", NecessaryPoints = 100 };

        _mockMapper.Setup(m => m.Map<EcoPontos.Domain.Reward.Reward>(createDto)).Returns(reward);
        _mockRewardRepository.Setup(r => r.CreateAsync(reward)).ReturnsAsync(reward);
        _mockMapper.Setup(m => m.Map<GetRewardDto>(reward)).Returns(expectedDto);

        // Act
        var result = await _rewardService.CreateRewardAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedDto.RewardId, result.RewardId);
        Assert.Equal(expectedDto.Description, result.Description);
        Assert.Equal(expectedDto.NecessaryPoints, result.NecessaryPoints);
    }

    [Fact]
    public async Task GetAllRewardsAsync_ShouldReturnListOfRewardDtos()
    {
        // Arrange
        var rewards = new List<EcoPontos.Domain.Reward.Reward>
        {
            new EcoPontos.Domain.Reward.Reward { RewardId = 1, Description = "Reward 1", NecessaryPoints = 100 },
            new EcoPontos.Domain.Reward.Reward { RewardId = 2, Description = "Reward 2", NecessaryPoints = 200 }
        };
        var rewardDtos = rewards.Select(r => new GetRewardDto { RewardId = r.RewardId, Description = r.Description, NecessaryPoints = r.NecessaryPoints });

        _mockRewardRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(rewards);
        _mockMapper.Setup(m => m.Map<GetRewardDto>(It.IsAny<EcoPontos.Domain.Reward.Reward>())).Returns((EcoPontos.Domain.Reward.Reward src) => new GetRewardDto { RewardId = src.RewardId, Description = src.Description, NecessaryPoints = src.NecessaryPoints });

        // Act
        var result = await _rewardService.GetAllRewardsAsync();

        // Assert
        Assert.Equal(rewards.Count, result.Count());
        Assert.Equal(rewardDtos.Select(x => x.RewardId), result.Select(x => x.RewardId));
    }

    [Fact]
    public async Task GetRewardByIdAsync_ShouldReturnRewardDto_WhenRewardExists()
    {
        // Arrange
        var reward = new EcoPontos.Domain.Reward.Reward { RewardId = 1, Description = "Reward 1", NecessaryPoints = 100 };
        var rewardDto = new GetRewardDto { RewardId = 1, Description = "Reward 1", NecessaryPoints = 100 };

        _mockRewardRepository.Setup(r => r.GetByIdAsync(reward.RewardId)).ReturnsAsync(reward);
        _mockMapper.Setup(m => m.Map<GetRewardDto>(reward)).Returns(rewardDto);

        // Act
        var result = await _rewardService.GetRewardByIdAsync(reward.RewardId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(rewardDto.RewardId, result.RewardId);
        Assert.Equal(rewardDto.Description, result.Description);
    }

    [Fact]
    public async Task UpdateRewardAsync_ShouldReturnTrue_WhenRewardExists()
    {
        // Arrange
        var reward = new EcoPontos.Domain.Reward.Reward { RewardId = 1, Description = "Reward 1", NecessaryPoints = 100 };
        var updateDto = new UpdateRewardDto { Description = "Updated Reward", NecessaryPoints = 150 };

        _mockRewardRepository.Setup(r => r.GetByIdAsync(reward.RewardId)).ReturnsAsync(reward);

        // Act
        var result = await _rewardService.UpdateRewardAsync(reward.RewardId, updateDto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteRewardAsync_ShouldReturnTrue_WhenRewardExists()
    {
        // Arrange
        var reward = new EcoPontos.Domain.Reward.Reward { RewardId = 1, Description = "Reward 1", NecessaryPoints = 100 };

        _mockRewardRepository.Setup(r => r.GetByIdAsync(reward.RewardId)).ReturnsAsync(reward);

        // Act
        var result = await _rewardService.DeleteRewardAsync(reward.RewardId);

        // Assert
        Assert.True(result);
    }
}