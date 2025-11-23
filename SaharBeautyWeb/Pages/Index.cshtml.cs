using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Pages.Shared;

namespace SaharBeautyWeb.Pages;

public class IndexModel : LandingBasePageModel
{
    public IndexModel(ErrorMessages errorMessage) : base(errorMessage)
    {
    }

    public void OnGet()
    {

    }

    public IActionResult OnGetClientComments(int pageNumber = 0)
    {
        return ViewComponent("ClientComments", new { pageNumber });
    }

    public IActionResult OnGetLandingGallery(int pageNumber = 0)
    {
        return ViewComponent("LandingGallery", new { pageNumber });
    }

}
