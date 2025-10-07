using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SaharBeautyWeb.Pages.Landing;

public class ErrorModel : PageModel
{
    public string? Message { get; set; }
    public void OnGet(string message)
    {
        Message = message ?? "خطایی پیش امده";
    }
}
