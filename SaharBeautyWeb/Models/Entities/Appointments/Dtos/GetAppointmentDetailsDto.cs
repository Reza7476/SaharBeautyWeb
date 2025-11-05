using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.Appointments.Dtos;

public class GetAppointmentDetailsDto
{
    public string? ClientName { get; set; }
    public string? ClientLastName { get; set; }
    public string ClientMobile { get; set; } = default!;
    public string TreatmentTitle { get; set; } = default!;
    public int Duration { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayWeek Day { get; set; }
    public decimal Price { get; set; }
    public AppointmentStatus Status { get; set; }
}
