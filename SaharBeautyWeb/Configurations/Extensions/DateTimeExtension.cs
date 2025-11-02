using System.Globalization;
using System;

namespace SaharBeautyWeb.Configurations.Extensions;

public static class DateTimeExtension
{
    public static string ToShamsi(this DateTime date)
    {
        var pc = new System.Globalization.PersianCalendar();
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

}
