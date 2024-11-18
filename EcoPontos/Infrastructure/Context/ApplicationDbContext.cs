using EcoPontos.Domain.Reward;
using EcoPontos.Domain.TaskRegister;
using EcoPontos.Domain.TaskWork;
using EcoPontos.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace EcoPontos.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Reward> Rewards { get; set; }
    public DbSet<TaskWork> TaskWorks { get; set; }
    public DbSet<TaskRegister> TaskRegisters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.TaskRegisters)
            .WithOne(tr => tr.User)
            .HasForeignKey(tr => tr.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
