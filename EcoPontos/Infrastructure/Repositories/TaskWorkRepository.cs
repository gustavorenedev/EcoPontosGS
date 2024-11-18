using EcoPontos.Domain.TaskWork;
using EcoPontos.Infrastructure.Context;
using EcoPontos.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EcoPontos.Infrastructure.Repositories;

public class TaskWorkRepository : ITaskWorkRepository
{
    private readonly ApplicationDbContext _context;

    public TaskWorkRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TaskWork> CreateAsync(TaskWork taskWork)
    {
        _context.TaskWorks.Add(taskWork);
        await _context.SaveChangesAsync();
        return taskWork;
    }

    public async Task<IEnumerable<TaskWork>> GetAllAsync()
    {
        return await _context.TaskWorks.ToListAsync();
    }

    public async Task<TaskWork?> GetByIdAsync(int id)
    {
        return await _context.TaskWorks.FindAsync(id);
    }

    public async Task DeleteAsync(TaskWork taskWork)
    {
        _context.TaskWorks.Remove(taskWork);
        await _context.SaveChangesAsync();
    }
}
