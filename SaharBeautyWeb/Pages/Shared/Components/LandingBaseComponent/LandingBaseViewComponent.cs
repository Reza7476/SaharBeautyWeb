using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;


public abstract class LandingBaseViewComponent : ViewComponent
{
    protected IViewComponentResult HandleApiResult<T>(ApiResultDto<T> result)
    {
        if (result == null)
            return Content("No response from server.");

        if (result.IsSuccess && (result.StatusCode == 200 || result.StatusCode == 204))
        {
            return View(result.Data); 
        }

        string errorMessage = !string.IsNullOrEmpty(result.Error)
            ? result.Error
            : "خطایی در سرور رخ داده است";

        string fallbackViewPath = "/Pages/Shared/Components/LandingBaseComponent/Default.cshtml";

        return View(fallbackViewPath, errorMessage);
    }
}
