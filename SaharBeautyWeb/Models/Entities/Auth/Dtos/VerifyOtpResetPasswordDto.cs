using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Auth.Dtos;

public class VerifyOtpResetPasswordDto
{
    [Required]
    public string OtpCode { get; set; } = default!;

    [Required]
    public string NewPassword { get; set; } = default!;

    [Required]
    public string OtpRequestId { get; set; } = default!;
}
