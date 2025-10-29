using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Treatments.Models;

public class GetTreatmentWithAllImagesDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<MediaDto> Media { get; set; } = default!;
    public IFormFile? Image { get; set; }
    public int Duration { get; set; }

}
