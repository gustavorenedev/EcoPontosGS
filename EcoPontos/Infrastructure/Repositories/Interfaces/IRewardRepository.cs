namespace EcoPontos.Infrastructure.Repositories.Interface;

public interface IRewardRepository
{
    Task<Domain.Reward.Reward> CreateAsync(Domain.Reward.Reward reward);
    Task<IEnumerable<Domain.Reward.Reward>> GetAllAsync();
    Task<Domain.Reward.Reward?> GetByIdAsync(int id);
    Task UpdateAsync(Domain.Reward.Reward reward);
    Task DeleteAsync(Domain.Reward.Reward reward);
}
