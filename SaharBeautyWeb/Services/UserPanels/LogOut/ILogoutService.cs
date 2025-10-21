using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Services.UserPanels.LogOut;

public interface ILogoutService : IService
{
    Task<ApiResultDto<object>> LogOutToken(string token);
}
