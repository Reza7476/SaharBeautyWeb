using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;

public class SaveScheduleModel
{
    public int Id { get; set; }
    public int DayOfWeek { get; set; }
    public bool IsActive { get; set; }

    [Required]
    public string StartTime { get; set; } = default!;

    [Required]
    public string EndTime { get; set; } = default!;
}
