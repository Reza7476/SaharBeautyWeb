using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;
using SaharBeautyWeb.Services.Treatments;

namespace SaharBeautyWeb.Pages.Landing.Treatments;

public class DetailsModel : PageModel
{
    private readonly ITreatmentService _service;
    public DetailsModel(ITreatmentService service)
    {
        _service = service;
    }


    public GetTreatmentDetailsModel Treatment { get; set; }
    public async Task<IActionResult> OnGet(long id)
    {
        var treatment = await _service.GetById(id);
        if (treatment.IsSuccess && treatment.Data != null)
        {
            Treatment = new GetTreatmentDetailsModel()
            {
                Description = treatment.Data.Description,
                Media = treatment.Data.Media,
                Title = treatment.Data.Title,
            };
            return Page();
        }
        else
        {
            return RedirectToPage("Index");
        }

    }
}
