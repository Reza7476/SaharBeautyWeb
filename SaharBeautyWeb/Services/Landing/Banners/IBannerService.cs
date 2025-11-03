using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Banners.Management.Dtos;

namespace SaharBeautyWeb.Services.Landing.Banners;

public interface IBannerService : IService
{
    Task<ApiResultDto<GetBannerDto?>> Get();
    Task<ApiResultDto<long>> Add(AddBannerDto dto);
    Task<ApiResultDto<long>> UpdateBanner(UpdateBannerDto dto);
}
