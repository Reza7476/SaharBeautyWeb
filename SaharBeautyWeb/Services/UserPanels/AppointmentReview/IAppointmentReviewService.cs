using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos.Clients;
using SaharBeautyWeb.Models.Entities.Review_Comment.Model;

namespace SaharBeautyWeb.Services.UserPanels.AppointmentReview;

public interface IAppointmentReviewService : IService
{
    Task<ApiResultDto<string>> AddClientReview(AddClientReviewAppointmentDto dto);
    Task<ApiResultDto<object>> ChangePublishStatus(string id, bool publishStatus);
    Task<ApiResultDto<GetAllDto<AppointmentReviewModel>>> GetAllComments(int offset, int limit);
    Task<ApiResultDto<GetAllDto<GetAllCommentLandingModel>>> GetAllPublishedComment(int offset, int limit);
}
