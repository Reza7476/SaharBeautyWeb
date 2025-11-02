namespace SaharBeautyWeb.Models.Entities.Appointments.Models.Clients;

public class GetAllMyAppointmentsModel
{
    public List<MyAppointmentsModel> MyAppointmentModel { get; set; } = new();
    public int TotalElements { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
