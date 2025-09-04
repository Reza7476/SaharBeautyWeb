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
}
