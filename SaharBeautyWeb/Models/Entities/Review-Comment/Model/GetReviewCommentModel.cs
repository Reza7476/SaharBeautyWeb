using SaharBeautyWeb.Models.Entities.Appointments.Models;

namespace SaharBeautyWeb.Models.Entities.Review_Comment.Model;

public class GetReviewCommentModel
{
    public List<AppointmentReviewModel> AllAppointmentReview { get; set; } = new();
    public int TotalElements { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
