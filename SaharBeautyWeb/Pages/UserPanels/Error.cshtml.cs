using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SaharBeautyWeb.Pages.UserPanels
{
    public class ErrorModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                ReturnUrl = "/UserPanels/Dashboard/Index";
            }
            return Page();
        }
    }
}
