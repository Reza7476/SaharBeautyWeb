namespace SaharBeautyWeb.Models.Entities.Appointments.Models;

public class TimeSlotModel
{
    public DateTime Start { get; set; } 
    public DateTime End { get; set; }

    public string Display => $"{Start:HH:mm} - {End:HH:mm}";
}
