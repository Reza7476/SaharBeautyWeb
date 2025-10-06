using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;
using SaharBeautyWeb.Services.Treatments;

namespace SaharBeautyWeb.Pages.Landing.Treatments
{
    public class IndexModel : PageModel
    {
        private readonly ITreatmentService _service;

        public GetAllTreatmentForLandingModel ListModel { get; set; } = new();

        public IndexModel(ITreatmentService service)
        {
            _service = service;
        }

        public async Task OnGet(int pageNumber = 0, int limit = 4)
        {
            int offset = pageNumber;
            var treatments = await _service.GetAll(offset, limit);
            if (treatments.IsSuccess && treatments.Data != null)
            {
                ListModel.Treatments = treatments.Data.Elements;
                ListModel.TotalElements = treatments.Data.TotalElements;
                ListModel.CurrentPage = pageNumber;
                ListModel.TotalPages = treatments.Data.TotalElements.ToTotalPage(limit);
            }
            else
            {
                ViewData["LandingErrorMessage"] = treatments.Error ?? "خطایی پیش آمده";
            }

        }
    }
}
