using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons;
using SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Treatments.Dtos;

namespace SaharBeautyWeb.Services.Treatments;

public interface ITreatmentService : IService
{
    Task<ApiResultDto<List<GetAllTreatmentDto>>> GetAll();
}
