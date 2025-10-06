using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;

public class GetTreatmentDetailsModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<MediaDto> Media { get; set; } = default!;
}
