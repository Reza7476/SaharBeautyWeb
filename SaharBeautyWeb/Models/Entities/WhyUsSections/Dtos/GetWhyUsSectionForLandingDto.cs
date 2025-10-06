using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;

public class GetWhyUsSectionForLandingDto
{
    public  string? Title { get; set; }
    public  string? Description { get; set; }
    public ImageDetailsDto Image { get; set; } = default!;

    public List<GetWhyUsQuestionDto> Questions { get; set; } = new();
}
