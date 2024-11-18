using System.ComponentModel.DataAnnotations;

namespace EcoPontos.Service.User.DTO;

public class LoginDto
{
    [Required]
    [StringLength(255)]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [Required]
    [StringLength(255)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
