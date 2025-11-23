using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Review_Comment.Model;
using SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;
using SaharBeautyWeb.Services.UserPanels.AppointmentReview;

namespace SaharBeautyWeb.Pages.Shared.Components.ClientComments;

public class ClientCommentsViewComponent : LandingBaseViewComponent
{

    private readonly IAppointmentReviewService _reviewService;

    public GetReviewPublishedCommentModel ReviewModel { get; set; } = new();

    public ClientCommentsViewComponent(
        ErrorMessages errorMessage,
        IAppointmentReviewService reviewService) : base(errorMessage)
    {
        _reviewService = reviewService;
    }
    public async Task<IViewComponentResult> InvokeAsync(int pageNumber = 0)
    {

        int offset = pageNumber;
        var limit = 10;
        var result = await _reviewService.GetAllPublishedComment(offset, limit);

        var response = HandleApiResult(result);
        if (result.IsSuccess && result.Data != null)
        {
            ReviewModel.AllAppointmentReview = result.Data.Elements;
            ReviewModel.TotalElements = result.Data.TotalElements;
            ReviewModel.CurrentPage = pageNumber;
            ReviewModel.TotalPages = result.Data.TotalElements.ToTotalPage(limit);
        }
        return View(ReviewModel);
    }

}
