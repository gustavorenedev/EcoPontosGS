using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoPontos.Domain.TaskWork;

[Table("TB_TaskWork_GS")]
public class TaskWork
{
    [Key]
    public int TaskWorkId { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
}
