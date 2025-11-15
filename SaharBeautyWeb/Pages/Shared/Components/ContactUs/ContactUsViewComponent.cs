using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;
using SaharBeautyWeb.Services.Landing.AboutUs;

namespace SaharBeautyWeb.Pages.Shared.Components.ContactUs;

public class ContactUsViewComponent : LandingBaseViewComponent
{
    private readonly IAboutUsService _aboutUsService;

    public ContactUsViewComponent(
        IAboutUsService aboutUsService,
        ErrorMessages errorMessage) : base(errorMessage)
    {
        _aboutUsService = aboutUsService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {

        var result = await _aboutUsService.GeAboutUs();
        if (result.IsSuccess && result.Data != null)
        {
            result.Data.Latitude ??= 29.6100;
            result.Data.Longitude ??= 52.5400;
        }
        return HandleApiResult(result);
    }
}
