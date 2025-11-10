using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Commons.Models;

public class GetAdminDashboardNewAppointmentsModel
{
    public string? ClientName { get; set; }
    public string? ClientLastName { get; set; }
    public string? TreatmentTitle { get; set; }
    public string? Mobile { get; set; }
    public string? Status { get; set; }
    public string? DayWeek { get; set; }
    public string? Date { get; set; }
}
