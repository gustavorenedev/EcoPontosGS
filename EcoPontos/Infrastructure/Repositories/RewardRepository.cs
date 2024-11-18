using EcoPontos.Domain.Reward;
using EcoPontos.Infrastructure.Context;
using EcoPontos.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EcoPontos.Infrastructure.Repositories;

public class RewardRepository : IRewardRepository
{
    private readonly ApplicationDbContext _context;

    public RewardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Reward> CreateAsync(Reward reward)
    {
        await _context.Set<Reward>().AddAsync(reward);
        await _context.SaveChangesAsync();
        return reward;
    }

    public async Task<IEnumerable<Reward>> GetAllAsync()
    {
        return await _context.Set<Reward>().ToListAsync();
    }

    public async Task<Reward?> GetByIdAsync(int id)
    {
        return await _context.Set<Reward>().FindAsync(id);
    }

    public async Task UpdateAsync(Reward reward)
    {
        _context.Set<Reward>().Update(reward);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Reward reward)
    {
        _context.Set<Reward>().Remove(reward);
        await _context.SaveChangesAsync();
    }
}