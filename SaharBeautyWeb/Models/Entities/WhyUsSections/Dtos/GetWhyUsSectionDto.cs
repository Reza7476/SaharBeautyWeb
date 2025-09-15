using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;

public class GetWhyUsSectionDto
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ImageDetailsDto? Image { get; set; }
    public List<GetWhyUsQuestionDto> Questions { get; set; } = new();
}
