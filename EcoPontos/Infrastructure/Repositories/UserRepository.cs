using EcoPontos.Domain.User;
using EcoPontos.Infrastructure.Context;
using EcoPontos.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EcoPontos.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserByEmailAsync(string? email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null) return null!;

        return user;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _context.Users
                                    .Include(u => u.TaskRegisters)
                                    .FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null) throw new Exception("User not found");

        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateScoreAsync(int userId, int seconds)
    {
        int pointsToAdd = (seconds / 60) * 10;

        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            throw new ArgumentException("Usuário não encontrado");
        }

        user.Points += pointsToAdd;

        await _context.SaveChangesAsync();

        return user.Points;
    }

}
