using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Commons.Dtos;

public class DashboardClientSummaryDto
{
    public List<DashboardClientAppointmentDto> FutureAppointments { get; set; } = new();
    public List<DashboardClientAppointmentDto> FormerAppointments { get; set; } = new();
}
public class DashboardClientAppointmentDto
{
    public string? TreatmentTitle { get; set; }
    public DayWeek Day { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public AppointmentStatus Status { get; set; }
}
