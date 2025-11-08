using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Enums;
using SaharBeautyWeb.Models.Entities.Appointments.Models;
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
            dto.AppointmentDate,
            dto.DayWeek
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PostAsync<string>(url, content);
        return result;

    }

    public async Task<ApiResultDto<string>> AddAdminAppointment(AddAdminAppointmentDto dto)
    {
        var url = $"{_apiUrl}/add-admin";
        var json = JsonSerializer.Serialize(new
        {
            dto.ClientId,
            dto.Duration,
            dto.TreatmentId,
            dto.AppointmentDate,
            dto.DayWeek
        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PostAsync<string>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> CancelByClient(string id)
    {
        var url = $"{_apiUrl}/{id}/cancel-by-client";

        var result = await PatchAsync<object>(url);
        return result;
    }

    public async Task<ApiResultDto<object>> ChangeStatus(string id, AppointmentStatus status)
    {
        var url = $"{_apiUrl}/change-status";
        var json = JsonSerializer.Serialize(new
        {
            id,
            status
        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PatchAsync<object>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetAllDto<GetAdminAllAppointmentsModel>>>
        GetAllAdminAppointments(
        int offset,
        int limit,
        AdminAppointmentFilterDto? filter = null,
        string? search = null)
    {
        var url = $"{_apiUrl}/all-admin";

        var query = new List<string>()
        {
            $"Offset={offset}",
            $"Limit={limit}",
        };
        if (!string.IsNullOrWhiteSpace(search))
        {
            query.Add($"search={search}");
        }

        if (filter != null)
        {
            if (filter.Date != null)
            {
                query.Add($"year={filter.Date.Value.Year}");
                query.Add($"month={filter.Date.Value.Month}");
                query.Add($"day={filter.Date.Value.Day}");
                query.Add($"dayOfWeek={filter.Date.Value.DayOfWeek}");
            }
            query.Add($"Status={filter.Status}");
            query.Add($"Day={filter.Day}");
            query.Add($"TreatmentTitle={filter.TreatmentTitle}");
        }

        if (query.Any())
        {
            url = url + "?" + string.Join("&", query);
        }

        var result = await GetAsync<GetAllDto<GetAdminAllAppointmentsModel>>(url);
        if (!result.IsSuccess || result.Error != null)
        {
            return new ApiResultDto<GetAllDto<GetAdminAllAppointmentsModel>>
            {
                Error = result.Error,
                IsSuccess = result.IsSuccess
            };
        }
        var mapped = new GetAllDto<GetAdminAllAppointmentsModel>()
        {
            Elements = result.Data.Elements,
            TotalElements = result.Data.TotalElements,
        };

        return new ApiResultDto<GetAllDto<GetAdminAllAppointmentsModel>>
        {
            Data = mapped,
            IsSuccess = true,
            StatusCode = result.StatusCode
        };
    }

    public async Task<ApiResultDto<GetAllDto<GetAdminAllAppointmentsModel>>>
        GetAllTodayAdminAppointments(int offset,
        int limit,
        AdminAppointmentFilterDto? filter = null,
        string? search = null)
    {
        var url = $"{_apiUrl}/all-today";
        var query = new List<string>()
        {
            $"Offset={offset}",
            $"Limit={limit}",
        };
        if (!string.IsNullOrWhiteSpace(search))
        {
            query.Add($"search={search}");
        }
        if (filter != null)
        {
            query.Add($"Status={filter.Status}");
            query.Add($"TreatmentTitle={filter.TreatmentTitle}");
        }
        if (query.Any())
        {
            url = url + "?" + string.Join("&", query);
        }
        var result = await GetAsync<GetAllDto<GetAdminAllAppointmentsModel>>(url);
        if (!result.IsSuccess || result.Error != null)
        {
            return new ApiResultDto<GetAllDto<GetAdminAllAppointmentsModel>>
            {
                Error = result.Error,
                IsSuccess = result.IsSuccess
            };
        }
        var mapped = new GetAllDto<GetAdminAllAppointmentsModel>()
        {
            Elements = result.Data.Elements,
            TotalElements = result.Data.TotalElements,
        };

        return new ApiResultDto<GetAllDto<GetAdminAllAppointmentsModel>>
        {
            Data = mapped,
            IsSuccess = true,
            StatusCode = result.StatusCode
        };
    }

    public async Task<ApiResultDto<List<GetBookedAppointmentByDateDto>>> GetBookedByDate(DateTime dateTime)
    {
        var url = $"{_apiUrl}/booked-appointment";

        var content = new MultipartFormDataContent();
        content.Add(new StringContent(dateTime.ToString() ?? ""), "date");


        var result = await GetAsync<List<GetBookedAppointmentByDateDto>>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetAppointmentDetailsDto?>> GetDetails(string id)
    {

        var url = $"{_apiUrl}/{id}";

        var result = await GetAsync<GetAppointmentDetailsDto?>(url);
        return result;
    }
}
