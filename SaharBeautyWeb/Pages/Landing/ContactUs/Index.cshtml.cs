using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.Landing.AboutUs;

namespace SaharBeautyWeb.Pages.Landing.ContactUs;

public class IndexModel : LandingBasePageModel
{
    private readonly IAboutUsService _aboutUsService;

    public GetAboutUsModel? ModelData { get; set; }

    public IndexModel(IAboutUsService aboutUsService,
        ErrorMessages errorMessage) : base(errorMessage)
    {
        _aboutUsService = aboutUsService;
    }

    public async Task<IActionResult> OnGet()
    {
        var result = await _aboutUsService.GeAboutUs();
        var response = HandleApiResult(result);
        if (result != null && result.IsSuccess && result.Data != null)
        {
            ModelData = new GetAboutUsModel
            {
                MobileNumber = result.Data.MobileNumber,
                Telephone = result.Data.Telephone,
                Email = result.Data.Email,
                Address = result.Data.Address,
                Description = result.Data.Description,
                Id = result.Data.Id,
                Instagram = result.Data.Instagram,
                Longitude = result.Data.Longitude ?? 52.5400,
                Latitude = result.Data.Latitude ?? 29.6100,
                LogoImage = result.Data.LogoImage != null ?
                        result.Data.LogoImage : null
            };
        }
        return response;
    }
}
