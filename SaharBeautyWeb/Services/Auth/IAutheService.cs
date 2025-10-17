using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Auth.Dtos;

namespace SaharBeautyWeb.Services.Auth;

public interface IAutheService : IService
{
    Task<ApiResultDto<GetTokenDto?>> LoginUser(LoginDto loginDto);
    Task<ApiResultDto<GetTokenDto?>> RefreshToken(string refreshToken, string token);
}
