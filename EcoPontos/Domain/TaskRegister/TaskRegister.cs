using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoPontos.Domain.TaskRegister;

[Table("TB_TaskRegister_GS")]
public class TaskRegister
{
    [Key]
    public int RegisterId { get; set; }
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User.User? User { get; set; }

    [ForeignKey("TaskWorkId")]
    public int TaskWorkId { get; set; }
    public TaskWork.TaskWork? Task { get; set; }
    public DateTime TaskDateCompleted { get; set; } = DateTime.Now;
    public int Duration { get; set; }
    public int PointsTotal { get; set; }
}
