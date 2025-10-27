using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Services.UserPanels.Admin.WeeklySchedules;

public interface IWeeklyScheduleService : IService
{
    Task<ApiResultDto<List<GetWeeklySchedulesDto>>> GetSchedules();
    Task<ApiResultDto<object>> Add(AddNewScheduleDto dto);
    Task<ApiResultDto<object>> Edit(EditScheduleDto dto);
    Task<ApiResultDto<GetScheduleDayDto?>> GetDaySchedule(DayWeek dayWeek);
}
