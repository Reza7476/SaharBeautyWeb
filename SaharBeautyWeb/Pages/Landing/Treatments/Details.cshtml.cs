using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.Landing.Treatments;

namespace SaharBeautyWeb.Pages.Landing.Treatments;

public class DetailsModel : LandingBasePageModel
{
    private readonly ITreatmentService _service;
    public DetailsModel(ITreatmentService service,
        ErrorMessages errorMessage) : base(errorMessage)
    {
        _service = service;
    }


    public GetTreatmentDetailsModel Treatment { get; set; }
    public async Task<IActionResult> OnGet(long id)
    {
        var result = await _service.GetById(id);
        var response = HandleApiResult(result);

        if (result.IsSuccess && result.Data != null)
        {
            Treatment = new GetTreatmentDetailsModel()
            {
                Description = result.Data.Description,
                Media = result.Data.Media,
                Title = result.Data.Title,
            };
        }
        return response;

    }
}
