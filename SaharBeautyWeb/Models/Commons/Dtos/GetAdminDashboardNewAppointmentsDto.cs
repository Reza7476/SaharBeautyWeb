using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Commons.Dtos;

public class GetAdminDashboardNewAppointmentsDto
{
    public string? ClientName { get; set; }
    public string? ClientLastName { get; set; }
    public string? TreatmentTitle { get; set; }
    public string? Mobile { get; set; }
    public AppointmentStatus Status { get; set; }
    public DayWeek DayWeek { get; set; }
    public DateOnly Date { get; set; }
}
