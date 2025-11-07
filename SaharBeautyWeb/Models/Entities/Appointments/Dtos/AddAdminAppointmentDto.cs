using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.Appointments.Dtos;

public class AddAdminAppointmentDto
{
    public long TreatmentId { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public int Duration { get; set; }
    public DayWeek DayWeek { get; set; }
    public string ClientId { get; set; } = default!;
}
