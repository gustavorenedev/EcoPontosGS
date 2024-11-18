using EcoPontos.Domain.User;

namespace EcoPontos.Infrastructure.Repositories.Interface;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByEmailAsync(string? email);
    Task<User> GetUserByIdAsync(int id);
    Task UpdateUserAsync(User user);
    Task<int> UpdateScoreAsync(int userId, int seconds);
}
