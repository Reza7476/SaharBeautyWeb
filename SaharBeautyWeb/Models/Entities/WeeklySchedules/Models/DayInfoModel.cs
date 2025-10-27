using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

namespace SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;

public class DayInfoModel
{
    public string PersianDay { get; set; } = "";
    public string PersianDate { get; set; } = "";
    public DayWeek Day { get; set; }
}
