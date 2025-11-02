using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos;

namespace SaharBeautyWeb.Services.UserPanels.Clients.Appointments;

public interface IAppointmentService : IService
{
    Task<ApiResultDto<string>> Add(AddAppointmentDto dto);

    Task<ApiResultDto<List<GetBookedAppointmentByDateDto>>> GetBookedByDate(DateTime date);
}
