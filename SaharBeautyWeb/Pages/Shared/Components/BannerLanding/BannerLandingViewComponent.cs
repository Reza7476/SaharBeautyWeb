using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;
using SaharBeautyWeb.Services.Banners;

namespace SaharBeautyWeb.Pages.Shared.Components.BannerLanding;

public class BannerLandingViewComponent : ViewComponent
{
    private readonly IBannerService _service;

    public BannerLandingViewComponent(IBannerService service)
    {
        _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var banner = await _service.Get();
        BannerLandingModel model;
        if (banner.IsSuccess && banner.Data != null)
        {
            model = new BannerLandingModel()
            {
                Title = (banner.Data.Title),
                CreateDate = (banner.Data.CreateDate),
                ImageName = (banner.Data.ImageName),
                URL = (banner.Data.URL),
            IsSuccess = banner.IsSuccess,
            StatusCode = banner.StatusCode
        };
        }
        else
        {
            model = new BannerLandingModel()
            {
                IsSuccess = false,
                Error = banner.Error,
                StatusCode = banner.StatusCode,
            };
        }

        return View(model);
    }
}




public class BannerLandingModel : ViewComponentErrorDto
{
    public string? Title { get; set; }
    public string? URL { get; set; }
    public string? ImageName { get; set; }
    public DateTime? CreateDate { get; set; }
}