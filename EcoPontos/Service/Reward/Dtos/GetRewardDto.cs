namespace EcoPontos.Service.Reward.Dtos;

public class GetRewardDto
{
    public int RewardId { get; set; }
    public string? Description { get; set; }
    public int NecessaryPoints { get; set; }
}
