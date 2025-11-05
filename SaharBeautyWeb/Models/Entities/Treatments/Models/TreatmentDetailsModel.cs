using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Treatments.Models;

public class TreatmentDetailsModel
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<MediaDto> Media { get; set; } = new();
    public IFormFile? AddMedia { get; set; }
    public int Duration { get; set; }
    public string? Price { get; set; }
}
