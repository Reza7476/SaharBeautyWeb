using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Services.UserPanels.UserFCMTokens;

public interface IUserFCMTokenService : IService
{
    Task<ApiResultDto<string>> Add(string token);
}
