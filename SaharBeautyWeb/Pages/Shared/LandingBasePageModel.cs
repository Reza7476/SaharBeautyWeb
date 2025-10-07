using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Pages.Shared;

public abstract class LandingBasePageModel : PageModel
{

    protected readonly ErrorMessages _errorMessage;

    protected LandingBasePageModel(ErrorMessages errorMessage)
    {
        _errorMessage = errorMessage;
    }

    protected IActionResult HandleApiResult<T>(ApiResultDto<T> result, string? successPage = null)
    {
        if (result == null) return RedirectToPage("/Landing/Error", new { message = "پاسخی از سرور دربافت نشد" });

        if (result.IsSuccess && (result.StatusCode == 200 || result.StatusCode == 204))
            return string.IsNullOrEmpty(successPage)
                ? Page()
                : RedirectToPage(successPage);


        string errorKey = result.Error ?? result.StatusCode.ToString();
        string message=_errorMessage.GetMessage(errorKey);
        

        return RedirectToPage("/Landing/Error", new { message });
    }

}
