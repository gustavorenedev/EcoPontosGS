using AutoMapper;
using EcoPontos.Infrastructure.Repositories.Interface;
using EcoPontos.Service.Reward.Dtos;

namespace EcoPontos.Service.Reward;

public class RewardService : IRewardService
{
    private readonly IRewardRepository _rewardRepository;
    private readonly IMapper _mapper;

    public RewardService(IRewardRepository rewardRepository, IMapper mapper)
    {
        _rewardRepository = rewardRepository;
        _mapper = mapper;
    }

    public async Task<GetRewardDto> CreateRewardAsync(CreateRewardDto dto)
    {
        var reward = _mapper.Map<Domain.Reward.Reward>(dto);
        var createdReward = await _rewardRepository.CreateAsync(reward);
        return _mapper.Map<GetRewardDto>(createdReward);
    }

    public async Task<IEnumerable<GetRewardDto>> GetAllRewardsAsync()
    {
        var rewards = await _rewardRepository.GetAllAsync();
        return rewards.Select(r => _mapper.Map<GetRewardDto>(r));
    }

    public async Task<GetRewardDto?> GetRewardByIdAsync(int id)
    {
        var reward = await _rewardRepository.GetByIdAsync(id);
        return reward != null ? _mapper.Map<GetRewardDto>(reward) : null;
    }

    public async Task<bool> UpdateRewardAsync(int id, UpdateRewardDto dto)
    {
        var reward = await _rewardRepository.GetByIdAsync(id);
        if (reward == null) return false;

        _mapper.Map(dto, reward);
        await _rewardRepository.UpdateAsync(reward);
        return true;
    }

    public async Task<bool> DeleteRewardAsync(int id)
    {
        var reward = await _rewardRepository.GetByIdAsync(id);
        if (reward == null) return false;

        await _rewardRepository.DeleteAsync(reward);
        return true;
    }
}