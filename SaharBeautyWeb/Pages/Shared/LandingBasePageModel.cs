using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Pages.Shared;

public abstract class LandingBasePageModel : PageModel
{

    protected IActionResult HandleApiResult<T>(ApiResultDto<T> result, string? successPage = null)
    {
        if (result == null) return RedirectToPage("/Landing/Error", new { message = "پاسخی از سرور دربافت نشد" });

        if (result.IsSuccess && (result.StatusCode == 200 || result.StatusCode == 204))


            return string.IsNullOrEmpty(successPage)
                ? Page()
                : RedirectToPage(successPage);
        string message = result.Error ?? result.StatusCode switch
        {
            400 => "درخواست نامعتبر است.",
            401 => "دسترسی غیرمجاز.",
            403 => "اجازه دسترسی ندارید.",
            404 => "موردی یافت نشد.",
            500 => "خطای داخلی سرور.",
            _ => "خطایی در پردازش درخواست رخ داده است."
        };

        return RedirectToPage("/Landing/Error", new { message });
    }

}
