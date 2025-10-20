using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Auth;

public class ResetPasswordStepTwoModel
{
    [Required(ErrorMessage = "رمز عبور را وارد کنید")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage = "تکرار رمز عبور را وارد کنید")]
    [Compare("Password", ErrorMessage = "رمز عبور و تکرار ان مطابقت ندارد")]
    [DataType(DataType.Password)]
    public string RepeatPassword { get; set; } = default!;

    [Required]
    public string OtpRequestId { get; set; } = default!;

    [Required(ErrorMessage = "کد تایید را وارد کنید")]
    public string OtpCode { get; set; } = default!;
}
