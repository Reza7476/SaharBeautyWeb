using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SaharBeautyWeb.Pages.UserPanels
{
    public class ErrorModel : PageModel
    {
        public string? Message { get; set; }
        public void OnGet(string message)
        {
            Message = message;
        }
    }
}
