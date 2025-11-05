using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.Appointments.Models;

public class GetAdminAllAppointmentsModel
{
    public string Id { get; set; } = default!;
    public string TreatmentTitle { get; set; } = default!;
    public int Duration { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayWeek DayWeek { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public string? ClientName { get; set; }
    public string?  ClientLastName { get; set; }
    public string?  ClientMobile { get; set; }
}
