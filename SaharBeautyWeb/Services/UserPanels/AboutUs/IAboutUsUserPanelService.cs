using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Dtos;

namespace SaharBeautyWeb.Services.UserPanels.AboutUs;

public interface IAboutUsUserPanelService : IService
{
    Task<ApiResultDto<long>> Add(AddAboutUsDto dto);
    Task<ApiResultDto<object>> Edit(EditAboutUsDto dto);
    Task<ApiResultDto<object>> EditLogo(EditMediaDto dto);
}
