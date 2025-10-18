using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Models;

namespace SaharBeautyWeb.Services.WhyUsSections;

public interface IWhyUsSectionService : IService
{
    Task<ApiResultDto<GetWhyUsSectionDto>> GetWhyUsSection();
    Task<ApiResultDto<WhyUsSectionModel_Edit?>> GetWhyUsSectionById(long id);
    Task <ApiResultDto<GetWhyUsSectionForLandingDto>> GetWhyUsSectionForLanding();
}
