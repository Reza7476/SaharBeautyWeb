using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.Appointments.Models;

public class AppointmentPerDayModel
{
    public string? DayWeek { get; set; }
    public int Count { get; set; }
}
