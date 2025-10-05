using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;

public class GetTreatmentsForLandingDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public long Id { get; set; }
    public MediaDto? Media { get; set; } = new();
}
