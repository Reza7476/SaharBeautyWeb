using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Models;

namespace SaharBeautyWeb.Services.UserPanels.Clients.Appointments;

public interface IAppointmentService : IService
{
    Task<ApiResultDto<string>> Add(AddAppointmentDto dto);
    Task<ApiResultDto<object>> CancelByClient(string id);
    
    Task<ApiResultDto<GetAllDto<GetAdminAllAppointmentsModel>>> 
        GetAllAdminAppointments(
        int offset,
        int limit,
        AdminAppointmentFilterDto? filter=null,
        string? search=null);
    
    Task<ApiResultDto<List<GetBookedAppointmentByDateDto>>> GetBookedByDate(DateTime date);
}
