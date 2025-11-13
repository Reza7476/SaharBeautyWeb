using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;
using System.Globalization;

namespace SaharBeautyWeb.Configurations.Extensions;

public static class DateTimeExtension
{
    public static string ConvertGregorianDateToShamsi(this DateTime date)
    {
        var pc = new PersianCalendar();
        return string.Format("{0:0000}/{1:00}/{2:00}",
                     pc.GetYear(date),
                     pc.GetMonth(date),
                     pc.GetDayOfMonth(date));
    }


    public static DateTime ConvertStringShamsiCalendarToGregorian(this string stringDate)
    {
        try
        {
            string[] parts = stringDate.Split('/');
            DateTime gregorianDate;
            var year = int.Parse(parts[0]);
            var month = int.Parse(parts[1]);
            var day = int.Parse(parts[2]);
            PersianCalendar pc = new PersianCalendar();
            gregorianDate = pc.ToDateTime(year, month, day, 0, 0, 0, 0);
            return gregorianDate;
        }
        catch (Exception)
        {

            throw new Exception();
        }
    }


    public static string ConvertDateOnlyToPersian(this DateOnly dateOnly)
    {
        if (dateOnly != default)
        {

            DateTime dateTime = dateOnly.ToDateTime(TimeOnly.MinValue);
            PersianCalendar pCalendar = new PersianCalendar();
            string persianDate = $"{pCalendar.GetYear(dateTime)}/{pCalendar.GetMonth(dateTime):00}/{pCalendar.GetDayOfMonth(dateTime):00}";
            return persianDate;
        }
        else
        {
            return " ";

        }
    }

    public static List<DayInfoModel> GeneratePersianWeekDays(int number = 7)
    {
        List<DayInfoModel> dayInfoModels = new List<DayInfoModel>();
        var persianCalendar = new PersianCalendar();
        var today = DateTime.UtcNow;

        string[] days = {
                "شنبه",
                "یک‌شنبه",
                "دوشنبه",
                "سه‌شنبه",
                "چهارشنبه",
                "پنج‌شنبه",
                "جمعه",
            };

        int dayOfWeek = ((int)persianCalendar.GetDayOfWeek(today) + 1) % 7;

        for (int i = 0; i < number; i++)
        {
            var dateUtc = today.AddDays(i);
            var currentDayOfWeek = dateUtc.DayOfWeek;
            var year = persianCalendar.GetYear(dateUtc);
            var month = persianCalendar.GetMonth(dateUtc);
            var day = persianCalendar.GetDayOfMonth(dateUtc);
            string persianDate = $"{day} {GetPersianMonthName(month)} {year}";

            dayInfoModels.Add(new DayInfoModel
            {
                PersianDay = days[(dayOfWeek + i) % 7],
                PersianDate = persianDate,
                Day = GetProjectDayWeek(currentDayOfWeek),
                Date = DateOnly.FromDateTime(dateUtc)
            });
        }
        return dayInfoModels;

    }
    private static string GetPersianMonthName(int month)
    {
        string[] months = {
                "فروردین",
                "اردیبهشت",
                "خرداد",
                "تیر",
                "مرداد",
                "شهریور",
                "مهر",
                "آبان",
                "آذر",
                "دی",
                "بهمن",
                "اسفند"
            };
        return months[month - 1];
    }

    private static DayWeek GetProjectDayWeek(DayOfWeek dayOfWeek)
    {
        switch (dayOfWeek)
        {
            case DayOfWeek.Saturday: return DayWeek.Saturday;
            case DayOfWeek.Sunday: return DayWeek.Sunday;
            case DayOfWeek.Monday: return DayWeek.Monday;
            case DayOfWeek.Tuesday: return DayWeek.Tuesday;
            case DayOfWeek.Wednesday: return DayWeek.Wednesday;
            case DayOfWeek.Thursday: return DayWeek.Thursday;
            case DayOfWeek.Friday: return DayWeek.Friday;
            default: return DayWeek.None;
        }
    }

    public static DayOfWeek GetSystemDayWeek(DayWeek dayweek)
    {
        switch (dayweek)
        {
            case DayWeek.Saturday: return DayOfWeek.Saturday;
            case DayWeek.Sunday: return DayOfWeek.Sunday;
            case DayWeek.Monday: return DayOfWeek.Monday;
            case DayWeek.Tuesday: return DayOfWeek.Tuesday;
            case DayWeek.Wednesday: return DayOfWeek.Wednesday;
            case DayWeek.Thursday: return DayOfWeek.Thursday;
            case DayWeek.Friday: return DayOfWeek.Friday;
            default: throw new Exception();
        }
    }

    public static string ConvertGregorianDateWithTimeToShamsi(this DateTime date)
    {
        var pc = new PersianCalendar();
        return string.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}",
                     pc.GetYear(date),
                     pc.GetMonth(date),
                     pc.GetDayOfMonth(date),
                     pc.GetHour(date),
                     pc.GetMinute(date));
            ;
    }
}
