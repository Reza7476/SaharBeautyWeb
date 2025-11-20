using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Pages.Shared;

public abstract class AjaxBasePageModel : UserPanelBaseModelPage
{

    public AjaxBasePageModel(ErrorMessages errorMessage) : base(errorMessage)
    {
    }
    protected IActionResult HandleApiAjxResult<T>(ApiResultDto<T> result, string? redirectUrl = null)
    {
        if (result == null)
        {
            return new JsonResult(new
            {
                error = "خطایی پیش آمده",
                StatusCode = 205,
                success = false,
                redirectUrl = redirectUrl
            });

        }
        if (result.IsSuccess && (result.StatusCode == 200 || result.StatusCode == 204))
        {
            return new JsonResult(new
            {
                StatusCode = result.StatusCode,
                success = result.IsSuccess,
                data = result.Data,
                redirectUrl = redirectUrl
            });
        }

        string errorKey = result.Error ??
                          result.StatusCode.ToString();
        string message = _errorMessage.GetMessage(errorKey);


        return new JsonResult(new
        {
            error = $"({result.StatusCode}) {message}",
            StatusCode = result.StatusCode,
            success = result.IsSuccess,
            redirectUrl = redirectUrl
        });

    }

    protected IActionResult HandleApiAjaxPartialResult<TModel, TData>(
        ApiResultDto<TData?> result,
        Func<TData, TModel> modelBuilder,
        string partialViewName)
    {
        if (result == null)
        {
            return new JsonResult(new
            {
                success = false,
                error = "پاسخی یافت نشد",
                statusCode = 500
            });
        }

        if (result.IsSuccess && result.Data != null)
        {
            var model = modelBuilder(result.Data);
            return new PartialViewResult
            {
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<TModel>(ViewData, model)
            };
        }

        string errorKey = result.Error ?? result.StatusCode.ToString();
        string message = _errorMessage.GetMessage(errorKey);

        return new JsonResult(new
        {
            success = false,
            error = $"({result.StatusCode}) {message}",
            statusCode = result.StatusCode
        });
    }

}
