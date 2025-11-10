using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Commons.Models;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.Appointments.Models;

namespace SaharBeautyWeb.Services.UserPanels.Clients.Appointments;

public interface IAppointmentService : IService
{
    Task<ApiResultDto<string>> Add(AddAppointmentDto dto);
    Task<ApiResultDto<string>> AddAdminAppointment(AddAdminAppointmentDto dto);
    Task<ApiResultDto<object>> CancelByClient(string id);
    Task<ApiResultDto<object>> ChangeStatus(string id, AppointmentStatus status);
    Task<ApiResultDto<GetDAshboardAdminSummaryDto?>> GetAdminDashboardSummary();
    Task<ApiResultDto<GetAllDto<GetAdminAllAppointmentsModel>>> 
        GetAllAdminAppointments(
        int offset,
        int limit,
        AdminAppointmentFilterDto? filter=null,
        string? search=null);
    
    Task<ApiResultDto<GetAllDto<GetAdminAllAppointmentsModel>>> 
        GetAllTodayAdminAppointments(
        int offset,
        int limit, 
        AdminAppointmentFilterDto? filter=null, 
        string? search=null);
    
    Task<ApiResultDto<List<GetAppointmentPerDayForChartDto>>> GetAppointmentPerDayForChart();
   
    Task<ApiResultDto<List<GetBookedAppointmentByDateDto>>> GetBookedByDate(DateTime date);
    Task<ApiResultDto<DashboardClientSummaryDto?>> GetDashboardClientSummary();
    Task<ApiResultDto<GetAppointmentDetailsDto?>> GetDetails(string id);
    Task<ApiResultDto<List<GetAdminDashboardNewAppointmentsDto>>> GetNewAppointmentDashboard();
}
