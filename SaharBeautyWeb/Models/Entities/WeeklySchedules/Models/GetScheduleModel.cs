using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;

public class GetScheduleModel
{
    public bool IsActive { get; set; }
    public DayWeek DayOfWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Id { get; set; }
}
