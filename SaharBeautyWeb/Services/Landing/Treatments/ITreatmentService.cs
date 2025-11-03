using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;

namespace SaharBeautyWeb.Services.Landing.Treatments;

public interface ITreatmentService : IService
{
    Task<ApiResultDto<GetAllDto<GetTreatmentDto>>> GetAll(int? offset = null, int? limit = null);
    Task<ApiResultDto<GetTreatmentWithAllImagesDto?>> GetById(long id);
    Task<ApiResultDto<List<GetTreatmentsForLandingDto>>> GetForLanding();
}
