using EcoPontos.Service.User.Dto;
using EcoPontos.Service.User.DTO;

namespace EcoPontos.Services.User;

public interface IUserService
{
    Task<string> Login(LoginDto dto);
    Task<RegisterUserDto> RegisterUser(RegisterUserDto dto);
    Task<int> Updatedscore(int userId, int seconds);
    Task<InfoUserReturn> GetUserById(int userId);

}
