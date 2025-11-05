using SaharBeautyWeb.Models.Entities.Appointments.Models.Clients;

namespace SaharBeautyWeb.Models.Entities.Appointments.Models;

public class GetAllAppointmentsModel
{
    public List<GetAdminAllAppointmentsModel> AllAppointmentModel { get; set; } = new();
    public int TotalElements { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
