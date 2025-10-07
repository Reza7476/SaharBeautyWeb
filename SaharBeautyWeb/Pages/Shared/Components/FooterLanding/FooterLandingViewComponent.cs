using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Services.AboutUs;

namespace SaharBeautyWeb.Pages.Shared.Components.FooterLanding;

public class FooterLandingViewComponent : ViewComponent
{

    private readonly IAboutUsService _aboutUsService;

    public FooterLandingViewComponent(IAboutUsService aboutUsService)
    {
        _aboutUsService = aboutUsService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var aboutUs = await _aboutUsService.GeAboutUs();
        if (aboutUs != null)
        {
            return View(aboutUs.Data);
        }
        return View();
    }
}
