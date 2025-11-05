using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Models;

namespace SaharBeautyWeb.Services.UserPanels.Admin.Treatments;

public interface ITreatmentUserPanelService : IService
{
    Task<ApiResultDto<long>> Add(AddTreatmentDto dto);
    Task<ApiResultDto<long>> AddImage(AddMediaDto dto);
    Task<ApiResultDto<object>> DeleteImage(long imageId, long id);
    Task<ApiResultDto<List<GetAllTreatmentForAppointmentModel>>> GetAllForAppointment();
    Task<ApiResultDto<GetTreatmentForAppointmentDto>> GetDetails(long id);
    Task<ApiResultDto<List<GetTreatmentTitleDto>>> GetTitlesForAdmin();
    Task<ApiResultDto<object>> UpdateTitleAndDescription(UpdateTreatmentTitleAndDescriptionDto dto);
}
