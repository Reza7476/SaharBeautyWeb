using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos.Clients;
using SaharBeautyWeb.Models.Entities.Appointments.Models.Clients;
using SaharBeautyWeb.Models.Entities.Clients.Dtos;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.UserPanels.Clients.ClientServices;

public class ClientService : UserPanelBaseService, IClientService
{

    private readonly string _apiUrl = "clients";
    public ClientService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<string>> AddNewClient(AddNewClientDto dto)
    {

        var url = $"{_apiUrl}/new";
        var json = JsonSerializer.Serialize(new
        {
            dto.Name,
            dto.LastName,
            dto.Mobile
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PostAsync<string>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetAllDto<MyAppointmentsModel>>>
         GetAllClientAppointments(int offset,
         int limit,
         ClientAppointmentFilterDto filter)
    {
        var url = $"{_apiUrl}/all-my-appointments";
        var query = new List<String>()
        {
            $"Offset={offset}",
            $"Limit={limit}"
        };

        if (filter.Date != null)
        {
            query.Add($"year={filter.Date.Value.Year}");
            query.Add($"month={filter.Date.Value.Month}");
            query.Add($"day={filter.Date.Value.Day}");
            query.Add($"dayOfWeek={filter.Date.Value.DayOfWeek}");
        }
        query.Add($"Status={filter.Status}");
        query.Add($"Day={filter.Day}");

        if (query.Any())
        {

            url = url + "?" + string.Join("&", query);
        }

        var result = await GetAsync<GetAllDto<MyAppointmentsModel>>(url);


        if (!result.IsSuccess || result.Error != null)
            return new ApiResultDto<GetAllDto<MyAppointmentsModel>>
            {
                Error = result.Error,
                IsSuccess = result.IsSuccess,
            };

        var mapped = new GetAllDto<MyAppointmentsModel>()
        {
            Elements = result.Data.Elements,
            TotalElements = result.Data.TotalElements
        };
        return new ApiResultDto<GetAllDto<MyAppointmentsModel>>
        {
            Data = mapped,
            IsSuccess = true,
            StatusCode = result.StatusCode
        };
    }

    public async Task<ApiResultDto<List<GetAllClientsForAddAppointmentDto>>> GetAllForAppointment(string? search=null)
    {
        var url = $"{_apiUrl}/all-for-create-appointment";

        var query = new List<string>();
        if (string.IsNullOrWhiteSpace(search))
        {
            query.Add($"search={search}");
        }
        url=url+"?" + string.Join("&", query);

        var result = await GetAsync<List<GetAllClientsForAddAppointmentDto>>(url);
        return result;

    }
}
