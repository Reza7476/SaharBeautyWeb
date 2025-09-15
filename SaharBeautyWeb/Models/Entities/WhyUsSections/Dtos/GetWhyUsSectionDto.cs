using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;

public class GetWhyUsSectionDto
{
    public long Id { get; set; }
    public required string Title { get; set; } 
    public required string Description { get; set; }
    public ImageDetailsDto? Image { get; set; }
    public List<GetWhyUsQuestionDto> Questions { get; set; } = new();
}
