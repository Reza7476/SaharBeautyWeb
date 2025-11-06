using SaharBeautyWeb.Models.Entities.Appointments.Enums;

namespace SaharBeautyWeb.Models.Entities.Appointments.Models;

public class GetAppointmentDetailsModel
{
    public string? ClientName { get; set; }
    public string? ClientLastName { get; set; }
    public string ClientMobile { get; set; } = default!;
    public string TreatmentTitle { get; set; } = default!;
    public string? Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string? Day { get; set; }
    public string? StatusString { get; set; }
    public AppointmentStatus Status { get; set; }
    public int Duration { get; set; }
    public string? Price { get; set; }
    public string Id { get; set; } = default!;
}
