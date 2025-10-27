
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.UserPanels.Admin.WeeklySchedules;

public class WeeklyScheduleService : UserPanelBaseService, IWeeklyScheduleService
{
    public WeeklyScheduleService(HttpClient client) : base(client)
    {
    }

    private const string _apiUrl = "weekly-schedules";

    public async Task<ApiResultDto<List<GetWeeklySchedulesDto>>> GetSchedules()
    {
        var url = $"{_apiUrl}";
        var result = await GetAsync<List<GetWeeklySchedulesDto>>(url);
        return result;
    }

    public async Task<ApiResultDto<object>> Add(AddNewScheduleDto dto)
    {
        var url = $"{_apiUrl}";
        var json = JsonSerializer.Serialize(new
        {
            dto.StartTime,
            dto.EndTime,
            dto.DayOfWeek,
            dto.IsActive
        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result=await PostAsync<object>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> Edit(EditScheduleDto dto)
    {
        var url = $"{_apiUrl}";
        var json = JsonSerializer.Serialize(new
        {
            dto.StartTime,
            dto.EndTime,
            dto.DayOfWeek,
            dto.IsActive,
            dto.Id
        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PutAsync<object>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetScheduleDayDto?>> GetDaySchedule(DayWeek dayWeek)
    {

        var url = $"{_apiUrl}/{dayWeek}/day-schedule";
        var result = await GetAsync<GetScheduleDayDto?>(url);
        return result;
    }
}
