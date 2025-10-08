using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Pages.Shared;

public abstract class AjaxBasePageModel: UserPanelBaseModelPage
{

    public AjaxBasePageModel(ErrorMessages errorMessage):base(errorMessage)
    {
    }
    protected IActionResult HandleApiAjxResult<T>(ApiResultDto<T> result,string? successPage = null)
    {
        if(result == null)
        {
            return new JsonResult(new
            {
                error = "خطایی پیش آمده",
                StatusCode = 205,
                success =false
            });

        }
        if(result.IsSuccess &&(result.StatusCode==200|| result.StatusCode == 204))
        {
            return new JsonResult(new
            {
                StatusCode = result.StatusCode,
                success = result.IsSuccess,
                data=result.Data
            });
        }

        string errorKey = result.Error ??
                          result.StatusCode.ToString();
        string message = _errorMessage.GetMessage(errorKey);


        return new JsonResult(new
        {
            error=$"({result.StatusCode}) {message}",
            StatusCode=result.StatusCode,
            success=result.IsSuccess
        });

    }
}
