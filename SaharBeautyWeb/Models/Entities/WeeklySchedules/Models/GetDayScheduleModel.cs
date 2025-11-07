using SaharBeautyWeb.Models.Entities.Appointments.Models;

namespace SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;

public class GetDayScheduleModel
{
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public List<TimeSlotModel> TimeSlots { get; set; } = new();
}
