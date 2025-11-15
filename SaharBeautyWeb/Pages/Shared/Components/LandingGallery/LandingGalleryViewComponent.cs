using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;

namespace SaharBeautyWeb.Pages.Shared.Components.LandingGallery;

public class LandingGalleryViewComponent : LandingBaseViewComponent
{
    public LandingGalleryViewComponent(ErrorMessages errorMessage) : base(errorMessage)
    {
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
