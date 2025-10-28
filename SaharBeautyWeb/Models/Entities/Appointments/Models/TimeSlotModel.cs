namespace SaharBeautyWeb.Models.Entities.Appointments.Models;

public class TimeSlotModel
{
    public TimeOnly Start { get; set; } 
    public TimeOnly End { get; set; }

    public bool IsActive { get; set; }
}
