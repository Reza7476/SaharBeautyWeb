
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos.Clients;
using System.Text.Json;
using System.Text;

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
}
