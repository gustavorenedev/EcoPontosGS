using EcoPontos.Domain.TaskRegister;
using EcoPontos.Infrastructure.Context;
using EcoPontos.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EcoPontos.Infrastructure.Repositories;

public class TaskRegisterRepository : ITaskRegisterRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRegisterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskRegister>> GetTasksByUserIdAsync(int userId)
    {
        return await _context.TaskRegisters
                         .Where(tr => tr.UserId == userId)
                         .Include(tr => tr.User)
                         .Include(tr => tr.Task)
                         .ToListAsync();
    }

    public async Task<TaskRegister> AddAsync(TaskRegister taskRegister)
    {
        await _context.TaskRegisters.AddAsync(taskRegister);
        await _context.SaveChangesAsync();
        return taskRegister;
    }

    public async Task<TaskRegister?> GetTaskRegisterAsync(int userId, int taskId)
    {
        return await _context.TaskRegisters
                             .Where(tr => tr.UserId == userId && tr.TaskWorkId == taskId)
                             .FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(TaskRegister taskRegister)
    {
        _context.TaskRegisters.Remove(taskRegister);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskRegister taskRegister)
    {
        // Verificar se a tarefa já está rastreada pelo contexto
        var existingTaskRegister = await _context.TaskRegisters
            .FirstOrDefaultAsync(t => t.UserId == taskRegister.UserId && t.TaskWorkId == taskRegister.TaskWorkId);

        if (existingTaskRegister != null)
        {
            // Atualizar as propriedades da tarefa com as novas informações
            existingTaskRegister.Duration = taskRegister.Duration;
            existingTaskRegister.TaskDateCompleted = taskRegister.TaskDateCompleted;

            // Salvar as alterações
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("TaskRegister não encontrado para atualização.");
        }
    }
}
