namespace SaharBeautyWeb.Models.Entities.Auth.Dtos;

public class VerifyOtpDto
{
    public string Name { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string OtpRequestId { get; set; } = default!;

    public string OtpCode { get; set; } = default!;

    public string? Email { get; set; }
}
