using Autofac.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Treatments.Dtos;
using SaharBeautyWeb.Services.Treatments;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Treatments
{
    public class IndexModel : PageModel
    {
        private readonly ITreatmentService _service;

        public IndexModel(ITreatmentService service)
        {
            _service = service;
        }
        public List<GetAllTreatmentDto> Treatments { get; set; } = new();

        [BindProperty]
        public AddTreatmentModel AddModel { get; set; }


        public async Task OnGet()
        {
            var treatments = await _service.GetAll();

            if (treatments.IsSuccess && treatments.Data != null)
            {
                Treatments = treatments.Data;
            }
            else
            {
                ViewData["ErrorMessage"] = treatments.Error ?? "خطایی پیش آمده";
            }
        }

        public PartialViewResult OnGetAddTreatmentPartial()
        {
            return Partial("_AddPartial");
        }

        public async Task <IActionResult> OnPostAddTreatment()
        {
            if (AddModel.Image == null ||
                string.IsNullOrWhiteSpace(AddModel.Title) ||
                string.IsNullOrWhiteSpace(AddModel.Description))
            {
                return new JsonResult(new
                {
                    success = false,
                    error = "فیلد های ناقص میباشند"
                });
            }

            var treatment = await _service.Add(new AddTreatmentModel
            {
                Description=AddModel.Description,
                Image=AddModel.Image,
                Title=AddModel.Title
            });

            return new JsonResult(new
            {
                data=treatment.Data,
                success=treatment.IsSuccess,
                statusCode=treatment.StatusCode,
                error=treatment.Error
            });
        }
    }
}
