using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.Appointments.Dtos;

public class GetAppointmentPerDayForChartDto
{
    public DayWeek DayWeek { get; set; }
    public int Count { get; set; }
}
