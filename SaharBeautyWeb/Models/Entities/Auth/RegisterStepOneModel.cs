using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Auth;

public class RegisterStepOneModel
{
    [Required(ErrorMessage = "شماره موبایل را وارد کنید")]
    public string MobileNumber { get; set; } = default!;
}
