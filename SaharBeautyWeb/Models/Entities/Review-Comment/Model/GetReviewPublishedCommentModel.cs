namespace SaharBeautyWeb.Models.Entities.Review_Comment.Model;

public class GetReviewPublishedCommentModel
{
    public List<GetAllCommentLandingModel> AllAppointmentReview { get; set; } = new();
    public int TotalElements { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
