
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos.Clients;
using System.Text.Json;
using System.Text;
using SaharBeautyWeb.Models.Entities.Review_Comment.Model;
using SaharBeautyWeb.Models.Entities.Appointments.Models;

namespace SaharBeautyWeb.Services.UserPanels.AppointmentReview;

public class AppointmentReviewService : UserPanelBaseService, IAppointmentReviewService
{
    private readonly string _apiUrl = "appointment-reviews";

    public AppointmentReviewService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<string>> AddClientReview(AddClientReviewAppointmentDto dto)
    {

        var url = $"{_apiUrl}";
        var json = JsonSerializer.Serialize(new
        {
            dto.Rate,
            dto.AppointmentId,
            dto.Description,
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PostAsync<string>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> ChangePublishStatus(string id, bool publishStatus)
    {
        var url = $"{_apiUrl}/change-publish-status";
        var json = JsonSerializer.Serialize(new
        {
            id,
            publishStatus,
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PatchAsync<object>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetAllDto<AppointmentReviewModel>>> 
        GetAllComments(int offset, int limit)
    {

        var url = $"{_apiUrl}/all-for-admin";

        var query = new List<string>()
        {
            $"Offset={offset}",
            $"Limit={limit}",
        };
        if (query.Any())
        {
            url = url + "?" + string.Join("&", query);
        }

        var result = await GetAsync<GetAllDto<AppointmentReviewModel>>(url);
        if (!result.IsSuccess || result.Error != null)
        {
            return new ApiResultDto<GetAllDto<AppointmentReviewModel>>
            {
                Error = result.Error,
                IsSuccess = result.IsSuccess
            };
        }
        var mapped = new GetAllDto<AppointmentReviewModel>()
        {
            Elements = result.Data.Elements,
            TotalElements = result.Data.TotalElements,
        };

        return new ApiResultDto<GetAllDto<AppointmentReviewModel>>
        {
            Data = mapped,
            IsSuccess = true,
            StatusCode = result.StatusCode
        };
    }
}
