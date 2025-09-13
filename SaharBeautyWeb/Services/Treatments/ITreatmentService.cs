using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Models;

namespace SaharBeautyWeb.Services.Treatments;

public interface ITreatmentService : IService
{
    Task<ApiResultDto<GetAllDto<GetTreatmentDto>>> GetAll(int? offset=null,int? limit=null);
    Task<ApiResultDto<long>> Add(AddTreatmentModel dto);
    Task<ApiResultDto<GetTreatmentWithAllImagesDto>> GetById(long id);
    Task<ApiResultDto<long>> AddImage(AddMediaDto dto);
    Task <ApiResultDto<object>> DeleteImage(long imageId, long id);
    Task <ApiResultDto<object>> UpdateTitleAndDescription(UpdateTreatmentTitleAndDescriptionDto dto);
}
