
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Users.Dtos;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.UserPanels.Users;

public class UserService : UserPanelBaseService, IUserService
{

    private const string _apiUrl = "users";
    public UserService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<object>> EditAdminProfile(EditAdminProfileDto dto)
    {

        var url = $"{_apiUrl}/admin-profile";

        var json = JsonSerializer.Serialize(new
        {
            dto.Name,
            dto.LastName,
            dto.Email,
            BirthDate = dto.BirthDateGregorian
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await PatchAsync<object>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> EditClientProfile(EditClientProfileDto dto)
    {
        var url = $"{_apiUrl}/client-profile";

        var json = JsonSerializer.Serialize(new
        {
            dto.Name,
            dto.LastName,
            dto.Email,
            BirthDate = dto.BirthDateGregorian,
            dto.UserName
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await PatchAsync<object>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> EditProfileImage(EditMediaDto dto)
    {
        var url = $"{_apiUrl}/profile-image";

        var content = new MultipartFormDataContent();
        var fileStream = dto.Media!.OpenReadStream();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.Media.ContentType);
        content.Add(fileContent, "Media", dto.Media.FileName);

        var result=await PatchAsync<object>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetUserInfoDto?>> GetUserInfo()
    {
        var url = $"{_apiUrl}";
        var result = await GetAsync<GetUserInfoDto?>(url);
        return result;
    }
}
