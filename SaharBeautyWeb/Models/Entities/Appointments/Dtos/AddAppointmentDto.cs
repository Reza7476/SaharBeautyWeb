using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Appointments.Dtos;

public class AddAppointmentDto
{
    public long? TreatmentId { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public int? Duration { get; set; }
}
