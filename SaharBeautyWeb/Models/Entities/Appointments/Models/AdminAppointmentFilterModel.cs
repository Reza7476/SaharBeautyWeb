using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.Appointments.Models;

public class AdminAppointmentFilterModel
{
    public DayWeek Day { get; set; }
    public string? Date { get; set; }
    public AppointmentStatus Status { get; set; }
    public string? TreatmentTitle { get; set; }
}
