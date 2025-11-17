
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Auth.Dtos;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.Auth;

public class AuthService : UserPanelBaseService, IAuthService
{
    private const string _apiUrl = "authentication";
    public AuthService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<GetTokenDto?>> LoginUser(LoginDto dto)
    {

        var url = $"{_apiUrl}/login";

        var json = JsonSerializer.Serialize(new
        {
            userName = dto.UserName,
            password = dto.Password,
        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await PostAsync<GetTokenDto?>(url, content);
        return result;

    }

    public async Task<ApiResultDto<GetTokenDto?>> RefreshToken(string refreshToken)
    {
        var url = $"{_apiUrl}/{refreshToken}/refresh-token";
        using var content = new StringContent("", Encoding.UTF8, "application/json");
        var result = await PostAsync<GetTokenDto?>(url,content);
        return result;
    }

    public async Task<ApiResultDto<GetOtpRequestForRegisterDto>> SendOtp(string mobileNumber)
    {
        var url = $"{_apiUrl}/initializing-register-user";
        var json = JsonSerializer.Serialize(new

        {
            MobileNumber = mobileNumber,
        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await PostAsync<GetOtpRequestForRegisterDto>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetOtpRequestForRegisterDto>> SendOtpResetPassword(string mobileNumber)
    {
        var url = $"{_apiUrl}/forget-pass-step-one";
        var json = JsonSerializer.Serialize(new
        {
            MobileNumber = mobileNumber,
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await PostAsync<GetOtpRequestForRegisterDto>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetTokenDto?>> VerifyOtp(VerifyOtpDto dto)
    {
        var url = $"{_apiUrl}/finalizing-register-user";

        var json = JsonSerializer.Serialize(new
        {
            Name = dto.Name,
            LastName = dto.LastName,
            UserName = dto.UserName,
            Password = dto.Password,
            OtpRequestId = dto.OtpRequestId,
            OtpCode = dto.OtpCode,
            Email = dto.Email
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await PostAsync<GetTokenDto?>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> VerifyOtpResetPassword(VerifyOtpResetPasswordDto dto)
    {
        var url = $"{_apiUrl}/forget-password-step-two";
        var json = JsonSerializer.Serialize(new
        {
            dto.NewPassword,
            dto.OtpRequestId,
            dto.OtpCode
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await PostAsync<object>(url, content);
        return result;
    }
}
