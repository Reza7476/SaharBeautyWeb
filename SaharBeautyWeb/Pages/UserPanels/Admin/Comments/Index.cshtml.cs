using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Review_Comment.Model;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.AppointmentReview;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.Comments;

public class IndexModel : AjaxBasePageModel
{

    private readonly IAppointmentReviewService _reviewService;
    public IndexModel(
        ErrorMessages errorMessage,
        IAppointmentReviewService reviewService) : base(errorMessage)
    {
        _reviewService = reviewService;
    }

    public GetReviewCommentModel ReviewModel { get; set; } = new();

    public async Task<IActionResult> OnGet(int pageNumber = 0, int limit = 10)
    {
        int offset = pageNumber;

        var result = await _reviewService.GetAllComments(offset, limit);
        var response = HandleApiResult(result);
        if (result.IsSuccess && result.Data != null)
        {
            ReviewModel.AllAppointmentReview = result.Data.Elements;
            ReviewModel.TotalElements = result.Data.TotalElements;
            ReviewModel.CurrentPage = pageNumber;
            ReviewModel.TotalPages = result.Data.TotalElements.ToTotalPage(limit);
        }

        return response;
    }

    public async Task<IActionResult> OnPostChangePublishStatus(string id,bool publishStatus)
    {
        var result = await _reviewService.ChangePublishStatus(id, publishStatus);
        var response = HandleApiAjxResult(result);
        return response;
    }


}
