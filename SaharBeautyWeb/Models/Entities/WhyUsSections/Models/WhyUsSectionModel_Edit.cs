using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.WhyUsSections.Models;

public class WhyUsSectionModel_Edit
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ImageDetailsDto? Image { get; set; }
    public long Id { get; set; }
    public IFormFile? AddMedia { get; set; }
}
