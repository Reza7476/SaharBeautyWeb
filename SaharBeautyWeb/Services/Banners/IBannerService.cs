using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons;
using SaharBeautyWeb.Models.Entities.Banners;

namespace SaharBeautyWeb.Services.Banners;

public interface IBannerService : IService
{
    Task<ApiResultDto<GetBannerDto?>> Get();
    Task<ApiResultDto<long>> Add(AddBannerModel dto); 
}
