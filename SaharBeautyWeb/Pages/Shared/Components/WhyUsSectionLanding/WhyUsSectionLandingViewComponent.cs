using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;
using SaharBeautyWeb.Services.WhyUsSections;

namespace SaharBeautyWeb.Pages.Shared.Components.WhyUsSectionLanding;

public class WhyUsSectionLandingViewComponent:ViewComponent
{
    private readonly IWhyUsSectionService _service;

    public WhyUsSectionLandingViewComponent(IWhyUsSectionService service)
    {
        _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var whyUs = await _service.GetWhyUsSectionForLanding();
        if (whyUs.IsSuccess && whyUs.Data!=null)
        {
            return View(whyUs.Data);
        }
        return View(new GetWhyUsSectionForLandingDto());  
    }
}
