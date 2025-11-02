using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos.Clients;
using SaharBeautyWeb.Models.Entities.Appointments.Models.Clients;

namespace SaharBeautyWeb.Services.UserPanels.Clients.ClientService;

public interface IClientService : IService
{
    Task<ApiResultDto<GetAllDto<MyAppointmentsModel>>>
      GetAllClientAppointments(
      int offset,
      int limit,
      ClientAppointmentFilterDto filter);
}
