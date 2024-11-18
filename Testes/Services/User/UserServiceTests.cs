using AutoMapper;
using EcoPontos.Infrastructure.Repositories.Interface;
using EcoPontos.Service.User.Dto;
using EcoPontos.Service.User.DTO;
using EcoPontos.Services.User;
using Moq;

namespace Testes.Services.User;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();
        _userService = new UserService(_mockUserRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnMappedUserInfo_WhenUserExists()
    {
        // Arrange
        int userId = 1;
        var user = new EcoPontos.Domain.User.User { UserId = userId, Name = "Test User" };
        var userInfoReturn = new InfoUserReturn { Name = "Test User" };

        _mockUserRepository.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);
        _mockMapper.Setup(mapper => mapper.Map<InfoUserReturn>(user)).Returns(userInfoReturn);

        // Act
        var result = await _userService.GetUserById(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userInfoReturn.Name, result.Name);
    }

    [Fact]
    public async Task Login_ShouldReturnSuccessMessage_WhenCredentialsAreValid()
    {
        // Arrange
        var dto = new LoginDto { Email = "test@example.com", Password = "password123" };
        var user = new EcoPontos.Domain.User.User { Email = dto.Email, Password = BCrypt.Net.BCrypt.HashPassword(dto.Password) };

        _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(dto.Email)).ReturnsAsync(user);

        // Act
        var result = await _userService.Login(dto);

        // Assert
        Assert.Equal("Login successful", result);
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnRegisteredUserDto_WhenNewUserIsRegistered()
    {
        // Arrange
        var dto = new RegisterUserDto { Name = "New User", Email = "teste@teste.com", Password = "Password@123", ConfirmPassword = "Password@123" };
        var user = new EcoPontos.Domain.User.User { Name = dto.Name, Email = dto.Email, Password = BCrypt.Net.BCrypt.HashPassword(dto.Password) };

        _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(dto.Email)).ReturnsAsync((EcoPontos.Domain.User.User)null!);
        _mockMapper.Setup(mapper => mapper.Map<EcoPontos.Domain.User.User>(It.IsAny<RegisterUserDto>())).Returns(user);
        _mockUserRepository.Setup(repo => repo.CreateUserAsync(It.IsAny<EcoPontos.Domain.User.User>())).ReturnsAsync(user);
        _mockMapper.Setup(mapper => mapper.Map<RegisterUserDto>(user)).Returns(dto);

        // Act
        var result = await _userService.RegisterUser(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(dto.Name, result.Name);
    }

    [Fact]
    public async Task Updatedscore_ShouldReturnUpdatedScore_WhenScoreIsUpdated()
    {
        // Arrange
        int userId = 1;
        int seconds = 100;
        int updatedScore = 10;

        _mockUserRepository.Setup(repo => repo.UpdateScoreAsync(userId, seconds)).ReturnsAsync(updatedScore);

        // Act
        var result = await _userService.Updatedscore(userId, seconds);

        // Assert
        Assert.Equal(updatedScore, result);
    }
}