namespace SaharBeautyWeb.Models.Entities.Appointments.Dtos;

public class GetBookedAppointmentByDateDto
{
    public TimeOnly StartDate { get; set; }
    public TimeOnly EndDate { get; set; }
    public int Duration { get; set; }
}
