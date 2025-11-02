using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.Appointments.Models.Clients;

public class ClientAppointmentFilterModel
{
    public DayWeek Day { get; set; }
    public string? Date { get; set; }
    public AppointmentStatus Status { get; set; }
}
