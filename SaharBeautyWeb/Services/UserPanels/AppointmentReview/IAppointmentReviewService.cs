using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos.Clients;

namespace SaharBeautyWeb.Services.UserPanels.AppointmentReview;

public interface IAppointmentReviewService : IService
{
    Task<ApiResultDto<string>> AddClientReview(AddClientReviewAppointmentDto dto);
}
