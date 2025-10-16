using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Auth;

public class LoginDataModel
{
    [Required]
    public string UserName { get; set; } = default!;
    
    [Required]
    public string Password { get; set; } = default!;
}
