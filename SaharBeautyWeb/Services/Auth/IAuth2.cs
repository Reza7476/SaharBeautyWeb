using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Auth.Dtos;

namespace SaharBeautyWeb.Services.Auth;

public interface IAuth2 : IService
{
    Task<ApiResultDto<GetTokenDto?>> LoginUser(LoginDto dto);
    Task<ApiResultDto<GetTokenDto?>> RefreshToken(string refreshToken);
    Task<ApiResultDto<GetOtpRequestForRegisterDto>> SendOtp(string mobileNumber);
    Task<ApiResultDto<GetOtpRequestForRegisterDto>> SendOtpResetPassword(string mobileNumber);
    Task<ApiResultDto<GetTokenDto?>> VerifyOtp(VerifyOtpDto dto);
    Task<ApiResultDto<object>> VerifyOtpResetPassword(VerifyOtpResetPasswordDto dto);
}
