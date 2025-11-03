using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;
using SaharBeautyWeb.Services.Landing.WhyUsSections;

namespace SaharBeautyWeb.Pages.Shared.Components.WhyUsSectionLanding;

public class WhyUsSectionLandingViewComponent : LandingBaseViewComponent
{
    private readonly IWhyUsSectionService _service;

    public WhyUsSectionLandingViewComponent(
        IWhyUsSectionService service,
        ErrorMessages errorMessage):base(errorMessage)
    {
        _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var whyUs = await _service.GetWhyUsSectionForLanding();
        return HandleApiResult(whyUs);
    }
}
