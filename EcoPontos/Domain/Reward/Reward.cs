using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoPontos.Domain.Reward;

[Table("TB_Reward_GS")]
public class Reward
{
    [Key]
    public int RewardId { get; set; }
    public string? Description { get; set; }
    public int NecessaryPoints { get; set; }
}
