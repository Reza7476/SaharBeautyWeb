namespace SaharBeautyWeb.Models.Entities.Review_Comment.Dto;

public class AppointmentReviewDto
{
    public string TreatmentTitle { get; set; } = default!;
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public bool IsPublished { get; set; }
    public byte Rate { get; set; }
}
