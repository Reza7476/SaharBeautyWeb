using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Auth;

public class RegisterStepTwoModel
{
    [Required(ErrorMessage ="نام خود را وارد کنید")]
    public string Name { get; set; } = default!;
    
    [Required(ErrorMessage ="نام خانوادگی خود را وارد کنید")]
    public string LastName { get; set; } = default!;
    
    public string? Email { get; set; }
    
    [Required(ErrorMessage ="کد تایید را وارد کنید")]
    public string OtpCode { get; set; } = default!;
    
    [Required(ErrorMessage ="نام کاربری را وارد کنید")]
    public string UserName { get; set; } = default!;

    [Required(ErrorMessage ="رمز عبور را وارد کنید")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage ="تکرار رمز عبور را وارد کنید")]
    [Compare("Password",ErrorMessage ="رمز عبور و تکرار ان مطابقت ندارد")]
    [DataType(DataType.Password)]
    public string RepeatPassword { get; set; } = default!;

    [Required]
    public string OtpRequestId { get; set; } = default!;


}
