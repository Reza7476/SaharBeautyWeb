using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Users.Dtos;

namespace SaharBeautyWeb.Services.UserPanels.Users;

public interface IUserService : IService
{
    Task<ApiResultDto<object>> EditAdminProfile(EditAdminProfileDto dto);
    Task<ApiResultDto<object>> EditProfileImage(EditMediaDto dto);
    Task<ApiResultDto<GetUserInfoDto?>> GetUserInfo();
}
