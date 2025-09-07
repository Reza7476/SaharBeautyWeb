using Microsoft.AspNetCore.Mvc.RazorPages;
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
    }
}
