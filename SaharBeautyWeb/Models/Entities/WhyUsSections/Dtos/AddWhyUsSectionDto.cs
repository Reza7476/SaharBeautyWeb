using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;

public class AddWhyUsSectionDto
{
    public required string Title { get; set; }
    public  required string Description { get; set; }
    public required IFormFile Image { get; set; }


}
