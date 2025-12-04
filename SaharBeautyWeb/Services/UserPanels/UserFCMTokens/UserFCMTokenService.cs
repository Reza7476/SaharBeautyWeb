
using SaharBeautyWeb.Models.Commons.Dtos;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.UserPanels.UserFCMTokens;

public class UserFCMTokenService : UserPanelBaseService, IUserFCMTokenService
{

    private const string _apiUrl = "user-fcm-token";
    public UserFCMTokenService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<string>> Add(string token)
    {
        var url = $"{_apiUrl}";

        var json = JsonSerializer.Serialize(new
        {
            FCMToken= token,
        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PostAsync<string>(url, content);
        return result;
    }
}

