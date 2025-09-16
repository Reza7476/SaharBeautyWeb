namespace SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;

public class AddWhyUsQuestionsDto
{
    public long WhyUsSectionId { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }
}
