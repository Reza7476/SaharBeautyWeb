using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Banners.Management.Dtos;
using SaharBeautyWeb.Models.Entities.Banners.Management.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.Banners;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Banners
{
    public class IndexModel :  AjaxBasePageModel
    {

        private readonly IBannerService _service;

        public IndexModel(IBannerService service,
            ErrorMessages errorMessage) : base(errorMessage)
        {
            _service = service;
        }

        public BannerModel? Banner { get; set; }

        [BindProperty]
        public AddBannerModel AddBanner { get; set; }
        [BindProperty]
        public EditBannerModel EditDto { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var result = await _service.Get();
            var response = HandleApiResult(result);
            if (result.IsSuccess)
            {
                Banner = result.Data != null ? new BannerModel()
                {
                    ImageName = result.Data.ImageName,
                    URL = result.Data.URL,
                    Id = result.Data.Id,
                    CreateDate = result.Data.CreateDate.ToShamsi(),
                    Title = result.Data.Title,
                } : null;
            }
            return response;
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
            var result = await _service.Add(new AddBannerDto()
            {
                Title = AddBanner.Title,
                Image = AddBanner.Image
            });
            var  response=HandleApiAjxResult(result);
            return response;
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
                data = updateBanner.Data,
                statusCode = updateBanner.StatusCode
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



    }
}
