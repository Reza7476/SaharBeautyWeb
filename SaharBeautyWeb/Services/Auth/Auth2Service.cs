
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Auth.Dtos;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.Auth;

public class Auth2Service : UserPanelBaseService, IAuth2
{
    private const string _apiUrl = "authentication";
    public Auth2Service(HttpClient client) : base(client)
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
}
