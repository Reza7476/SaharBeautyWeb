using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Auth.Dtos;
using SaharBeautyWeb.Services.Contracts;
using System.Net.Http.Headers;

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
}
