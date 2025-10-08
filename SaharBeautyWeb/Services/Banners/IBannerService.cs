using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Banners.Management.Dtos;
using SaharBeautyWeb.Models.Entities.Banners.Management.Models;

namespace SaharBeautyWeb.Services.Banners;

public interface IBannerService : IService
{
    Task<ApiResultDto<GetBannerDto?>> Get();
    Task<ApiResultDto<long>> Add(AddBannerModel dto);
    Task<ApiResultDto<long>> UpdateBanner(UpdateBannerDto dto);
}
