namespace SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

public class AddNewScheduleDto
{
    public bool IsActive { get; set; }
    public DayWeek DayOfWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

}
