namespace SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;

public class GetWhyUsQuestionDto
{
    public long Id { get; set; }
    public string Question { get; set; } = default!;
    public string Answer { get; set; } = default!;
}
