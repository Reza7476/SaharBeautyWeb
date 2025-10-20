using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Auth.Dtos;
using SaharBeautyWeb.Services.Contracts;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.Auth;

public class AuthService : IAutheService
{

    private readonly HttpClient _httpClient;
    private readonly ICRUDApiService _apiService;
    private const string _apiUrl = "Users";

    public AuthService(
        HttpClient httpClient,
        ICRUDApiService apiService,
        string? baseAddress)
    {
        _httpClient = httpClient;
        _apiService = apiService;
    }

    public async Task<ApiResultDto<GetTokenDto?>> LoginUser(LoginDto dto)
    {
        var url = $"{_apiUrl}/login";

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.UserName), "UserName");
        content.Add(new StringContent(dto.Password), "Password");

        var result = await _apiService.AddFromFormAsync<GetTokenDto?>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetTokenDto?>> RefreshToken(string refreshToken, string token)
    {
        var url = $"{_apiUrl}/{refreshToken}/refresh-token";
        
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
        requestMessage.Headers.Authorization=new AuthenticationHeaderValue("Bearer", token);

        var result = await _apiService.SendFromRoutAsyncAsPost<GetTokenDto?>(requestMessage);
        return result;
    }

    public async Task<ApiResultDto<GetOtpRequestForRegisterDto>> SendOtp(string mobileNumber)
    {
        var url = $"{_apiUrl}/initializing-register-user";
        var json = new
        {
            MobileNumber = mobileNumber,
        };

        var result = await _apiService.AddFromBodyAsync<GetOtpRequestForRegisterDto>(url, json);
        return result;
    }

    public async Task<ApiResultDto<GetTokenDto?>> VerifyOtp(VerifyOtpDto dto)
    {
        var url = $"{_apiUrl}/finalizing-register-user";

        var json = new
        {
            Name = dto.Name,
            LastName = dto.LastName,
            UserName = dto.UserName,
            Password = dto.Password,
            OtpRequestId = dto.OtpRequestId,
            OtpCode = dto.OtpCode,
            Email = dto.Email
        };
        var result = await _apiService.AddFromBodyAsync<GetTokenDto?>(url, json);
        return result;
    }

    public async Task<ApiResultDto<GetOtpRequestForRegisterDto>> 
        SendOtpResetPassword(string mobileNumber)
    {
        var url = $"{_apiUrl}/forget-pass-step-one";
        var json = new
        {
            MobileNumber = mobileNumber,
        };

        var result = await _apiService
            .AddFromBodyAsync<GetOtpRequestForRegisterDto>(url, json);
        return result;
    }

    public async Task<ApiResultDto<object>> VerifyOtpResetPassword(VerifyOtpResetPasswordDto dto)
    {

        var url = $"{_apiUrl}/forget-password-step-two";
        var json = new
        {
            dto.NewPassword,
            dto.OtpRequestId,
            dto.OtpCode
        };

        var result = await _apiService.AddFromBodyAsync<object>(url, json);
        return result;
    }
}
