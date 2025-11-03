
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Users.Dtos;

namespace SaharBeautyWeb.Services.UserPanels.Users;

public class UserService : UserPanelBaseService, IUserService
{

    private const string _apiUrl = "users";
    public UserService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<GetUserInfoDto>> GetUserInfo()
    {
        var url = $"{_apiUrl}";
        var result = await GetAsync<GetUserInfoDto>(url);
        return result;
    }
}
