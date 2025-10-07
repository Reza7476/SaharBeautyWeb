﻿using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;
using SaharBeautyWeb.Services.Treatments;

namespace SaharBeautyWeb.Pages.Shared.Components.FirstTreatmentLanding;

public class FirstTreatmentLandingViewComponent : LandingBaseViewComponent
{
    private readonly ITreatmentService _service;

    // تزریق همزمان ITreatmentService و ErrorMessages
    public FirstTreatmentLandingViewComponent(ITreatmentService service,
        ErrorMessages errorMessages)
        : base(errorMessages)
    {
        _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var treatment = await _service.GetForLanding();
        return HandleApiResult(treatment);
    }
}
