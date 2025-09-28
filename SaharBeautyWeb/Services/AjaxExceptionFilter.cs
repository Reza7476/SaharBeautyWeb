using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AjaxExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (IsAjaxRequest(context.HttpContext.Request))
        {
            // خطا برای درخواست AJAX
            context.Result = new JsonResult(new
            {
                redirect = "/Error",
                message = "خطای غیرمنتظره، دوباره تلاش کنید"
            });
            context.HttpContext.Response.StatusCode = 500;
        }
        else
        {
            // خطا برای درخواست معمولی (غیر AJAX)
            context.Result = new RedirectToPageResult("/Error");
        }

        context.ExceptionHandled = true;
    }

    private bool IsAjaxRequest(HttpRequest request)
    {
        return request.Headers["X-Requested-With"] == "XMLHttpRequest" ||
               request.Headers["Accept"].ToString().Contains("application/json");
    }
}
