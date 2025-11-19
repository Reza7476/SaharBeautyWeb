namespace SaharBeautyWeb.Models.Entities.Appointments.Dtos.Clients;

public class AddClientReviewAppointmentDto
{
    public byte Rate { get; set; }
    public string? Description { get; set; }
    public string AppointmentId { get; set; }=default!;
}
