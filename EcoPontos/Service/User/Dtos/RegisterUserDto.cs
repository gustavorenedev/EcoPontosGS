using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcoPontos.Service.User.DTO;

public class RegisterUserDto
{
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    [Required]
    [StringLength(255)]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [Required]
    [StringLength(255)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    [JsonIgnore]
    public DateTime RegisterDate { get; set; } = DateTime.Now;
    [JsonIgnore]
    public int Points { get; set; } = 0;
}
