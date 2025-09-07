using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons;
using SaharBeautyWeb.Models.Entities.Banners;
using SaharBeautyWeb.Services.Banners;
using System.Text.Json;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Banners
{
    public class IndexModel : PageModel
    {

        private readonly IBannerService _service;

        public IndexModel(IBannerService service)
        {
            _service = service;
        }

        public BannerDto Banner { get; set; }

        [BindProperty]
        public AddBannerModel AddBanner { get; set; }
        [BindProperty]
        public EditBannerModel EditDto { get; set; }

        public async Task OnGet()
        {
            var banner = await _service.Get();
            if (banner.IsSuccess && banner.Data != null)
            {

                Banner = new BannerDto()
                {
                    ImageName = banner.Data.ImageName,
                    URL = banner.Data.URL,
                    Id = banner.Data.Id,
                    IsSuccess = banner.IsSuccess,
                    StatusCode = banner.StatusCode,
                    CreateDate = banner.Data.CreateDate.ToShamsi(),
                    Title = banner.Data.Title,
                    Error = banner.Error
                };
            }
            else
            {
                Banner = new BannerDto()
                {
                    IsSuccess = banner.IsSuccess,
                    StatusCode = banner.StatusCode,
                    Error = banner.Error,
                    URL = null
                };
            }
        }


        public async Task<IActionResult> OnPostCreateBanner()
        {
            if (AddBanner.Image == null || string.IsNullOrWhiteSpace(AddBanner.Title))
            {
                return new JsonResult(new
                {
                    success = false,
                    error = "عنوان یا عکس ناقص است"
                });
            }
            var banner = await _service.Add(new AddBannerModel()
            {
                Title = AddBanner.Title,
                Image = AddBanner.Image
            });

            return new JsonResult(new
            {
                data = banner.Data,
                success = banner.IsSuccess,
                statusCode = banner.StatusCode,
                error = banner.Error
            });
        }


        public async Task<PartialViewResult> OnGetEditBannerPartial()
        {
            var banner = await _service.Get();

            var model = new EditBannerModel()
            {
                Id = banner.Data.Id,
                Title = banner.Data.Title,
                ImageUrl = banner.Data.URL
            };

            return Partial("_Edit", model);
        }

        public async Task<IActionResult> OnPostEditBanner()
        {
            if (EditDto.Image == null || string.IsNullOrEmpty(EditDto.Title))
            {
                return new JsonResult(new
                {
                    success = false,
                    error = "عنوان یا عکس ناقص است"
                });
            }

            var updateBanner = await _service.UpdateBanner(new UpdateBannerDto
            {
                Id = EditDto.Id,
                Image = EditDto.Image,
                Title = EditDto.Title
            });

            return new JsonResult(new
            {
                success = updateBanner.IsSuccess,
                error = updateBanner.Error,
                data= updateBanner.Data,
                statusCode= updateBanner.StatusCode
            });
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
        public class BannerDto
        {
            public bool IsSuccess { get; set; }
            public int StatusCode { get; set; }
            public long Id { get; set; }
            public string? ImageName { get; set; }
            public string? URL { get; set; }
            public string? CreateDate { get; set; }
            public string? Title { get; set; }
            public string? Error { get; set; }
        }

        public class EditBannerModel
        {
            public long Id { get; set; }
            public string? Title { get; set; }
            public string? ImageUrl { get; set; }
            public IFormFile? Image { get; set; }
        }
    }
}
