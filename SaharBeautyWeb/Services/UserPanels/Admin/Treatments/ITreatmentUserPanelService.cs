﻿using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Models;

namespace SaharBeautyWeb.Services.UserPanels.Treatments;

public interface ITreatmentUserPanelService : IService
{
    Task<ApiResultDto<long>> Add(AddTreatmentModel dto);
    Task<ApiResultDto<long>> AddImage(AddMediaDto dto);
    Task<ApiResultDto<object>> DeleteImage(long imageId, long id);
    Task<ApiResultDto<List<GetAllTreatmentForAppointmentModel>>> GetAllForAppointment();
    Task<ApiResultDto<GetTreatmentForAppointmentDto>> GetDetails(long id);
    Task<ApiResultDto<object>> UpdateTitleAndDescription(UpdateTreatmentTitleAndDescriptionDto dto);
}
