using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Treatments.Dtos;

public class GetTreatmentDto
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public MediaDto? Media { get; set; }
    public decimal Price { get; set; }
}

