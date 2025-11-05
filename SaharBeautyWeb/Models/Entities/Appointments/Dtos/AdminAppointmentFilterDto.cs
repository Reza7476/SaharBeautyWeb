using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.Appointments.Dtos;

public class AdminAppointmentFilterDto
{
    public DayWeek Day { get; set; }
    public DateOnly? Date { get; set; }
    public AppointmentStatus Status { get; set; }
    public string? TreatmentTitle { get; set; }
}
