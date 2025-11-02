using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.Appointments.Dtos.Clients;

public class ClientAppointmentFilterDto
{
    public DayWeek Day { get; set; }
    public DateOnly? Date { get; set; }
    public AppointmentStatus Status { get; set; }

}
