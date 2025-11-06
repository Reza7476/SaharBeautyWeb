using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Clients.Models;

public class GetAllClientsForAddAppointmentModel
{
    public string MobileNumber { get; set; } = default!;
    public string Id { get; set; } = default!;
    public ImageDetailsDto? Profile { get; set; } 
    public string? Name { get; set; }
    public string? LastName { get; set; }
}
