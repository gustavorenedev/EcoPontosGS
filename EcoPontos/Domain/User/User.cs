using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoPontos.Domain.User;

[Table("TB_User_GS")]
public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    [Required]
    [StringLength(255)]
    public string? Email { get; set; }
    [Required]
    [StringLength(255)]
    public string? Password { get; set; }
    public DateTime RegisterDate { get; set; } = DateTime.Now;
    public int Points { get; set; }
    public ICollection<TaskRegister.TaskRegister>? TaskRegisters { get; set; }
}