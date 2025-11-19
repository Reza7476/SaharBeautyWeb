namespace SaharBeautyWeb.Models.Entities.Appointments.Models.Clients;

public class AddClientReviewModel
{
    public byte Rate { get; set; }
    public string? Description { get; set; }
    public string AppointmentId { get; set; } = default!;
}
