using System.Globalization;
using System.Text.RegularExpressions;

namespace SaharBeautyWeb.Configurations.Extensions;

public static class StringExtension
{

    public static string ConvertPersianNumberToEnglish(this string number)
    {

        if (string.IsNullOrWhiteSpace(number))
            return number;
        return number
        .Replace('۰', '0')
        .Replace('۱', '1')
        .Replace('۲', '2')
        .Replace('۳', '3')
        .Replace('۴', '4')
        .Replace('۵', '5')
        .Replace('۶', '6')
        .Replace('۷', '7')
        .Replace('۸', '8')
        .Replace('۹', '9');
    }

    public static decimal ConvertStringNumberToDecimal(this string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            return 0m;
        number = number.ConvertPersianNumberToEnglish();

        number = number.Replace(",", "").Replace("٬", "");
        decimal price=decimal.Parse(number, CultureInfo.InvariantCulture);
        return price;
    }
}
