
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Users.Dtos;
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

    public async Task<ApiResultDto<GetUserInfoDto?>> GetUserInfo()
    {
        var url = $"{_apiUrl}";
        var result = await GetAsync<GetUserInfoDto?>(url);
        return result;
    }
}
