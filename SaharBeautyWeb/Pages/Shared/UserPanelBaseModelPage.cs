using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Pages.Shared;

public class UserPanelBaseModelPage : PageModel
{
    protected readonly ErrorMessages _errorMessage;

    public UserPanelBaseModelPage(ErrorMessages errorMessage)
    {
        _errorMessage = errorMessage;
    }


    protected IActionResult HandleApiResult<T>(
        ApiResultDto<T> result,
        string? successPage = null)
    {
        if (result == null)
        {
            return RedirectToPage("/UserPanels/Error",
                new
                {
                    message = "پاسخی از سرور دریافت نشد",
                    returnUrl = Request.Path
                });
        }

        if (result.IsSuccess &&
           (result.StatusCode == 200 ||
           result.StatusCode == 204))
        {
            return string.IsNullOrWhiteSpace(successPage)
                ? Page()
                : RedirectToPage(successPage);
        }

        string errorKey = result.Error ?? result.StatusCode.ToString();
        string message = _errorMessage.GetMessage(errorKey);

        return RedirectToPage("/UserPanels/Error", new
        {
            message,
            returnUrl = Request.Path
        });
    }
}
