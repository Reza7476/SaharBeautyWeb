using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Treatments.Dtos;

public class GetTreatmentForAppointmentDto
{
    public int Duration { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ImageDetailsDto Image { get; set; } = default!;
}
