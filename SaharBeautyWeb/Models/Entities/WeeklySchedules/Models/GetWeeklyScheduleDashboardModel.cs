namespace SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;

public class GetWeeklyScheduleDashboardModel
{
    public string? Start { get; set; }
    public string? End { get; set; }
    public string? Day { get; set; }
    public bool IsActive { get; set; }
}
