using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Treatments.Dtos;

public class AddTreatmentDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public IFormFile Image { get; set; } = default!;
    public int Duration { get; set; }
    public decimal Price { get; set; }
}
