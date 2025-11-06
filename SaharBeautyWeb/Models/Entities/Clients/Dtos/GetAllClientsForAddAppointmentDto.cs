using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Clients.Dtos;

public class GetAllClientsForAddAppointmentDto
{
    public string MobileNumber { get; set; } = default!;
    public string Id { get; set; } = default!;
    public ImageDetailsDto Profile { get; set; } = new();
    public string? Name { get; set; }
    public string? LastName { get; set; }

}
