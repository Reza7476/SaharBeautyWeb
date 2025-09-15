using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.WhyUsSections.Models;

public class WhyUsSectionModel
{
    public long Id { get; set; }
    public string? Title { get; set; } 
    public string? Description { get; set; } 
    public ImageDetailsDto? Media { get; set; }
    public IFormFile? Image { get; set; }
    public List<WhyUsQuestionModel> QuestionModels { get; set; } = new();
}

public class WhyUsQuestionModel
{
    public long Id { get; set; }
    public string Question { get; set; } = default!;
    public string Answer { get; set; } = default!;
}
