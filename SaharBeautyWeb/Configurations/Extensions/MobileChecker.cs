namespace SaharBeautyWeb.Configurations.Extensions;

public static class MobileChecker
{
    public static (bool isValid, string message) CheckMobile(this string mobile)
    {
        if (string.IsNullOrEmpty(mobile))
        {
            return (false, "MobileIsInvalid");
        }
        mobile = mobile.Trim();
        if (mobile.Length != 11 || !mobile.StartsWith("0"))
        {
            return (false, "شماره موبایل نامعتبر است شکل صحیح '09171111111'");
        }

        return (true,"+98"+mobile.Substring(1));
    }
}
