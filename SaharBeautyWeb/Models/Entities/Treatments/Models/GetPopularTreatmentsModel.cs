using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Treatments.Models;

public class GetPopularTreatmentsModel
{
    public ImageDetailsDto? Image { get; set; }
    public string? Title { get; set; }
    public int AppointmentCount { get; set; }
    public long Id { get; set; }
}
