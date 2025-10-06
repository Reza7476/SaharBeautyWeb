using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;
using SaharBeautyWeb.Services.Treatments;

namespace SaharBeautyWeb.Pages.Shared.Components.FirstTreatmentLanding;

public class FirstTreatmentLandingViewComponent : ViewComponent
{
    private readonly ITreatmentService _service;

    public FirstTreatmentLandingViewComponent(ITreatmentService service)
    {
        _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {

        var treatment = await _service.GetForLanding();

        if (treatment.IsSuccess && treatment.Data != null)
        {
            return View(treatment.Data);
        }
        return View(new List<GetTreatmentsForLandingDto>());
    }
}
