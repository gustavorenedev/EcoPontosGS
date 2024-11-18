using AutoMapper;
using EcoPontos.Infrastructure.Repositories.Interface;
using EcoPontos.Service.User.Dto;
using EcoPontos.Service.User.DTO;

namespace EcoPontos.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<InfoUserReturn> GetUserById(int userId)
    {
        var result = await _userRepository.GetUserByIdAsync(userId);

        return _mapper.Map<InfoUserReturn>(result);
    }

    public async Task<string> Login(LoginDto dto)
    {
        var user = await _userRepository.GetUserByEmailAsync(dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        return "Login successful";
    }

    public async Task<RegisterUserDto> RegisterUser(RegisterUserDto dto)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);

        if (dto.Password != dto.ConfirmPassword) throw new Exception("passwords are not the same");

        if (existingUser != null)
        {
            throw new InvalidOperationException("Email already in use.");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var clientWithHashedPassword = new RegisterUserDto
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = hashedPassword
        };

        var newUser = _mapper.Map<Domain.User.User>(clientWithHashedPassword);

        var createdUser = await _userRepository.CreateUserAsync(newUser);

        return _mapper.Map<RegisterUserDto>(createdUser);
    }

    public async Task<int> Updatedscore(int userId, int seconds)
    {
        return await _userRepository.UpdateScoreAsync(userId, seconds);
    }
}
