using Autofac.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Dtos;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Models;
using SaharBeautyWeb.Services.AboutUs;
using System.Net;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.AboutUs
{
    public class IndexModel : PageModel
    {

        private readonly IAboutUsService _service;

        public GetAboutUsModel ModelData { get; set; }

        [BindProperty]
        public AddAboutUsDto AddAboutUsDto { get; set; }

        [BindProperty]
        public EditAboutUsModel EditModel { get; set; }

        public IndexModel(IAboutUsService service)
        {
            _service = service;
        }

        public async Task OnGet()
        {
            var result = await _service.GeAboutUs();
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
            else if (!result.IsSuccess)
            {
                ViewData["ErrorMessage"] = result.Error ?? "خطایی پیش آمده";
            }
            else
            {
                ModelData = new GetAboutUsModel()
                {
                    MobileNumber = string.Empty
                };
            }
        }

        public async Task<IActionResult> OnPostCreateAboutUs()
        {
            var mobile = AddAboutUsDto.MobileNumber.CheckMobile();
            if (!mobile.isValid)
            {
                return new JsonResult(new
                {
                    success = false,
                    error = mobile.message
                });
            }
            var result = await _service.Add(new AddAboutUsDto()
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

            return new JsonResult(new
            {

                success = result.IsSuccess,
                data = result.Data,
                error = result.Error,
                statusCode = result.StatusCode
            });
        }

        public async Task<IActionResult> OnGetGetAboutUsForEdit(long id)
        {
            var result = await _service.GeAboutUsById(id);
            if (result.IsSuccess && result.Data != null)
            {
                var model = new EditAboutUsModel()
                {
                    MobileNumber = result.Data.MobileNumber,
                    Address = result.Data.Address,
                    Description = result.Data.Description,
                    Email = result.Data.Email,
                    Id = result.Data.Id,
                    Instagram = result.Data.Instagram,
                    Latitude = result.Data.Latitude,
                    Longitude = result.Data.Longitude,
                    Telephone = result.Data.Telephone
                };

                return Partial("_EditAboutUsPartial", model);
            }
            else
            {
                return new JsonResult(new
                {
                    data = result.Data,
                    success = result.IsSuccess,
                    error = result.Error,
                    ststusCode = result.StatusCode
                });
            }
        }


        public async Task<IActionResult> OnPostApplyEditAboutUs()
        {
            var result = await _service.Edit(new EditAboutUsDto()
            {
                MobileNumber=EditModel.MobileNumber,
                Address=EditModel.Address,
                Description=EditModel.Description,
                Email=EditModel.Email,
                Id= EditModel.Id,
                Instagram=EditModel.Instagram,
                Latitude=EditModel.Latitude,
                Longitude=EditModel.Longitude,
                Telephone=EditModel.Telephone
            });
            return new JsonResult(new
            {
                success = result.IsSuccess,
                error = result.Error,
                statusCode=result.StatusCode,
                Data=result.Data
            });
        }
    }
}

