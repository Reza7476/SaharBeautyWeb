namespace SaharBeautyWeb.Models.Entities.Auth.Dtos;

public class GetOtpRequestForRegisterDto
{
    public string? OtpRequestId { get; set; }
    public int VerifyStatusCode { get; set; }
    public string? VerifyStatus { get; set; }
}
