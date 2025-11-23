namespace SaharBeautyWeb.Models.Entities.Review_Comment.Model;

public class GetAllCommentLandingModel
{
    public string TreatmentTitle { get; set; } = default!;
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public byte Rate { get; set; }
}
