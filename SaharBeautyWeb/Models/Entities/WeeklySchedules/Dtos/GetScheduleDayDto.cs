namespace SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

public class GetScheduleDayDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
}
