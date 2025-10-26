﻿namespace SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;

public class GetWeeklySchedulesDto
{
    public bool IsActive { get; set; }
    public DayWeek DayOfWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Id { get; set; }
}
public enum DayWeek : byte
{
    Saturday = 0,
    Sunday = 1,
    Monday = 2,
    Tuesday = 3,
    Wednesday = 4,
    Thursday = 5,
    Friday = 6,
}
