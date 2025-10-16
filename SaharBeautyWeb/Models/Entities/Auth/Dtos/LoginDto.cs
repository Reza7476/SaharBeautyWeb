using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Auth.Dtos;

public class LoginDto
{
    [Required]
    public string UserName { get; set; } = default!;
    [Required]
    public string Password { get; set; } = default!;
}
