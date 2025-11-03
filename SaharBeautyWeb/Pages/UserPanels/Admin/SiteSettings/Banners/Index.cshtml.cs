using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Banners.Management.Dtos;
using SaharBeautyWeb.Models.Entities.Banners.Management.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.Landing.Banners;
using SaharBeautyWeb.Services.UserPanels.Admin.Banners;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Banners
{
    public class IndexModel : AjaxBasePageModel
    {

        private readonly IBannerService _service;
        private readonly IBannerUserPanelService _bannerUserPanelService;
        public IndexModel(IBannerService service,
            ErrorMessages errorMessage,
            IBannerUserPanelService bannerUserPanelService) : base(errorMessage)
        {
            _service = service;
            _bannerUserPanelService = bannerUserPanelService;
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

            var (isValid, message) = AddBanner.Image.ValidateImage();
            if (!isValid)
                return new JsonResult(new
                {
                    success = false,
                    error = message
                });
            if(string.IsNullOrWhiteSpace(AddBanner.Title))
                {
                return new JsonResult(new
                {
                    success = false,
                    error = "عنوان نباید خالی باشد "
                });
            }
            var result = await _bannerUserPanelService.Add(new  AddBannerDto()
            {
                Title = AddBanner.Title,
                Image = AddBanner.Image,
               
            });
            var response = HandleApiAjxResult(result);
            return response;
        }


        public async Task<IActionResult> OnGetEditBannerPartial()
        {
            var result = await _service.Get();
            return HandleApiAjaxPartialResult(
                result, data => new EditBannerModel()
                {
                    Id = data.Id,
                    Title = data.Title,
                    ImageUrl = data.URL
                }, "_Edit");
        }

        public async Task<IActionResult> OnPostEditBanner()
        {
            var (isValid, message) = EditDto.Image.ValidateImage();
            if (!isValid)
                return new JsonResult(new
                {
                    success = false,
                    error = message
                });

            if (string.IsNullOrEmpty(EditDto.Title))
            
                return new JsonResult(new
                {
                    success = false,
                    error = "عنوان یا نیابد خالی باشد"
                });

            var result = await _bannerUserPanelService.UpdateBanner(new UpdateBannerDto
            {
                Id = EditDto.Id,
                Image = EditDto.Image,
                Title = EditDto.Title
            });
            var response = HandleApiAjxResult(result);
            return response;
            
        }
    }
}
