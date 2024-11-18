using AutoMapper;
using EcoPontos.Service.Reward.Dtos;

namespace EcoPontos.Service.Reward.Mapping;

public class RewardProfile : Profile
{
    public RewardProfile()
    {
        CreateMap<Domain.Reward.Reward, GetRewardDto>();
        CreateMap<CreateRewardDto, Domain.Reward.Reward>();
        CreateMap<UpdateRewardDto, Domain.Reward.Reward>();
    }
}
