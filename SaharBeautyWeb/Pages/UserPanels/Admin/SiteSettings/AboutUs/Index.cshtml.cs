using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Dtos;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.AboutUs;
using SaharBeautyWeb.Services.UserPanels.AboutUs;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.AboutUs
{
    public class IndexModel : AjaxBasePageModel
    {
        private readonly IAboutUsService _service;

        private readonly IAboutUsUserPanelService _aboutUsUSerPanelService;

        public GetAboutUsModel? ModelData { get; set; }

        [BindProperty]
        public AddAboutUsDto? AddAboutUsDto { get; set; }

        [BindProperty]
        public EditAboutUsModel? EditModel { get; set; }

        public IndexModel(IAboutUsService service,
            ErrorMessages errorMessage,
            IAboutUsUserPanelService aboutUsUSerPanelService) : base(errorMessage)
        {
            _service = service;
            _aboutUsUSerPanelService = aboutUsUSerPanelService;
        }

        public async Task<IActionResult> OnGet()
        {
            var result = await _service.GeAboutUs();
            var response = HandleApiResult(result);

            if (result.IsSuccess && result.Data != null)
            {
                ModelData = new GetAboutUsModel()
                {
                    MobileNumber = result.Data.MobileNumber,
                    Telephone = result.Data.Telephone,
                    Email = result.Data.Email,
                    Address = result.Data.Address,
                    Description = result.Data.Description,
                    Id = result.Data.Id,
                    Instagram = result.Data.Instagram,
                    Longitude = result.Data.Longitude,
                    Latitude = result.Data.Latitude,
                    LogoImage = result.Data.LogoImage != null ?
                     result.Data.LogoImage : null
                };
            }
            return response;
        }

        public async Task<IActionResult> OnPostCreateAboutUs()
        {
            var mobile = AddAboutUsDto!.MobileNumber.CheckMobile();
            if (!mobile.isValid)
            {
                return new JsonResult(new
                {
                    success = false,
                    error = mobile.message
                });
            }
            if (AddAboutUsDto.LogoImage != null)
            {
                var (isValid, message) = AddAboutUsDto.LogoImage.ValidateImage();
                if (!isValid)
                {
                    return new JsonResult(new
                    {
                        success = isValid,
                        error = message
                    });
                }
            }
            var result = await _aboutUsUSerPanelService.Add(new AddAboutUsDto()
            {
                MobileNumber = AddAboutUsDto.MobileNumber,
                Telephone = AddAboutUsDto.Telephone,
                Address = AddAboutUsDto.Address,
                Description = AddAboutUsDto.Description,
                Email = AddAboutUsDto.Email,
                Longitude = AddAboutUsDto.Longitude,
                Latitude = AddAboutUsDto.Latitude,
                Instagram = AddAboutUsDto.Instagram,
                LogoImage = AddAboutUsDto.LogoImage,
            });

            return HandleApiAjxResult(result);
        }

        public async Task<IActionResult> OnGetGetAboutUsForEdit(long id)
        {
            var result = await _service.GeAboutUsById(id);
            return HandleApiAjaxPartialResult(result, data => new EditAboutUsModel()
            {
                MobileNumber = data.MobileNumber,
                Address = data.Address,
                Description = data.Description,
                Email = data.Email,
                Id = data.Id,
                Instagram = data.Instagram,
                Latitude = data.Latitude,
                Longitude = data.Longitude,
                Telephone = data.Telephone
            }, "_EditAboutUsPartial");
        }


        public async Task<IActionResult> OnPostApplyEditAboutUs()
        {

            var mobile = EditModel!.MobileNumber.CheckMobile();
            if (!mobile.isValid)
            {
                return new JsonResult(new
                {
                    success = false,
                    error = mobile.message
                });
            }
            var result = await _aboutUsUSerPanelService.Edit(new EditAboutUsDto()
            {
                MobileNumber = EditModel.MobileNumber,
                Address = EditModel.Address,
                Description = EditModel.Description,
                Email = EditModel.Email,
                Id = EditModel.Id,
                Instagram = EditModel.Instagram,
                Latitude = EditModel.Latitude,
                Longitude = EditModel.Longitude,
                Telephone = EditModel.Telephone
            });

            return HandleApiAjxResult(result);
        }

        public async Task<IActionResult> OnGetGetLogoById(long id)
        {
            var result = await _service.GeAboutUsById(id);
            return HandleApiAjaxPartialResult(result, data => new EditAboutUsModel()
            {
                MobileNumber = data.MobileNumber,
                LogoDetails = data.LogoImage != null ? new ImageDetailsDto()
                {
                    Extension = data.LogoImage.Extension,
                    ImageName = data.LogoImage.ImageName,
                    UniqueName = data.LogoImage.UniqueName,
                    Url = data.LogoImage.Url
                } : null,
                Id = data.Id
            }, "_editLogo");

        }

        public async Task<IActionResult> OnPostApplyEditedAboutUsLogo()
        {
            var (isValid, message) = EditModel!.Logo.ValidateImage();

            if (EditModel?.Id == null || !isValid)
            {
                return new JsonResult(new
                {
                    error = message,
                    success = false
                });
            }
            var result = await _aboutUsUSerPanelService.EditLogo(new EditMediaDto()
            {
                Media = EditModel.Logo,
                Id = EditModel.Id
            });
            return HandleApiAjxResult(result);
        }
    }
}

