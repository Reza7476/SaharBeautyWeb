using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Banners.Management.Dtos;

namespace SaharBeautyWeb.Services.UserPanels.Banners;

public interface IBannerUserPanelService : IService
{
    Task<ApiResultDto<long>> Add(AddBannerDto dto);
    Task<ApiResultDto<long>> UpdateBanner(UpdateBannerDto updateBannerDto);
}
