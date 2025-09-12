using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments;
using SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Treatments.Dtos;

namespace SaharBeautyWeb.Services.Treatments;

public interface ITreatmentService : IService
{
    Task<ApiResultDto<List<GetAllTreatmentDto>>> GetAll();
    Task<ApiResultDto<long>> Add(AddTreatmentModel dto);
    Task<ApiResultDto<GetTreatmentWithAllImagesDto>> GetById(long id);
    Task<ApiResultDto<long>> AddImage(AddMediaDto dto);
}
