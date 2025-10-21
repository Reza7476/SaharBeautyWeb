
using SaharBeautyWeb.Models.Commons.Dtos;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.UserPanels.LogOut;

public class LogoutService : UserPanelBaseService, ILogoutService
{
    private const string _apiUrl = "Users";

    public LogoutService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<object>> LogOutToken(string token)
    {
        var url = $"{_apiUrl}/log-out";
        var json = JsonSerializer.Serialize(new
        {
            RefreshToken = token,
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PatchAsync<object>(url, content);
        return result;
    }
}
