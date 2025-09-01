using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Entities.Banners;
using SaharBeautyWeb.Services.Banners;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Banners
{
    public class IndexModel : PageModel
    {

        private readonly IBannerService _service;

        public IndexModel(IBannerService service)
        {
            _service = service;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetBannerAsync()
        {
            var banner = await _service.Get();
            return new JsonResult(new
            {
                data = banner.Data,
                success = banner.IsSuccess,
                statusCode = banner.StatusCode,
                error = banner.Error
            });

        }

        public async Task<IActionResult> OnPostBannerAsync(
            string Title,
            IFormFile? Image = null
            )
        {
            if (Image == null || string.IsNullOrEmpty(Title))
            {
                return new JsonResult(new { success = false, error = "عنوان یا تصویر ناقص است" });
            }
            var banner = await _service.Add(new AddBannerModel
            {
                Image = Image,
                Title = Title
            });

            return new JsonResult(new
            {
                data = banner.Data,
                success = banner.IsSuccess,
                statusCode = banner.StatusCode,
                error = banner.Error
            });
        }
    }
}
