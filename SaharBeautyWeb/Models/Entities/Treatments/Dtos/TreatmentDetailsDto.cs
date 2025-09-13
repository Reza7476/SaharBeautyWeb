using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Treatments.Dtos;

public class TreatmentDetailsDto
{
    public long  Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<MediaDto> Media { get; set; } = default!;
    public IFormFile? AddMedia  { get; set; }
}
