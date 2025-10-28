
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.UserPanels.Clients.Appointments;

public class AppointmentService : UserPanelBaseService, IAppointmentService
{
    private readonly string _apiUrl = "appointments";

    public AppointmentService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<string>> Add(AddAppointmentDto dto)
    {
        var url = $"{_apiUrl}";
        var json = JsonSerializer.Serialize(new
        {
            dto.TreatmentId,
            dto.Duration,
            dto.AppointmentDate
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result=await PostAsync<string>(url, content);
        return result;

    }
}
