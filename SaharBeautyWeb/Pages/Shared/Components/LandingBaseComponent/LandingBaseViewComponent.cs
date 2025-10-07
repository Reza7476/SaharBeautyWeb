using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;


public abstract class LandingBaseViewComponent : ViewComponent
{

    protected readonly ErrorMessages _errorMessage;

    protected LandingBaseViewComponent(ErrorMessages errorMessage)
    {
        _errorMessage = errorMessage;
    }

    protected IViewComponentResult HandleApiResult<T>(ApiResultDto<T> result)
    {
        if (result == null)
            return Content("No response from server.");

        if (result.IsSuccess && (result.StatusCode == 200 || result.StatusCode == 204))
        {
            return View(result.Data); 
        }

        string errorKey = result.Error ?? result.StatusCode.ToString();
        string errorMessage = _errorMessage.GetMessage(errorKey);

        string fallbackViewPath = "/Pages/Shared/Components/LandingBaseComponent/Default.cshtml";

        return View(fallbackViewPath, errorMessage);
    }
}
