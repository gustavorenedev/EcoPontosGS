using EcoPontos.Service.Reward.Dtos;

namespace EcoPontos.Service.Reward;

public interface IRewardService
{
    Task<GetRewardDto> CreateRewardAsync(CreateRewardDto dto);
    Task<IEnumerable<GetRewardDto>> GetAllRewardsAsync();
    Task<GetRewardDto?> GetRewardByIdAsync(int id);
    Task<bool> UpdateRewardAsync(int id, UpdateRewardDto dto);
    Task<bool> DeleteRewardAsync(int id);
}
