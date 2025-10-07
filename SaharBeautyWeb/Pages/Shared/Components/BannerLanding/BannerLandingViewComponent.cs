using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Banners.Landing;
using SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;
using SaharBeautyWeb.Services.Banners;

namespace SaharBeautyWeb.Pages.Shared.Components.BannerLanding;

public class BannerLandingViewComponent : LandingBaseViewComponent
{
    private readonly IBannerService _service;

    public BannerLandingViewComponent(
        IBannerService service,
        ErrorMessages errorMessage):base(errorMessage)
    {
        _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var banner = await _service.Get();

        var model = new ApiResultDto<BannerLandingModel>()
        {
            Data = banner.Data != null ? new BannerLandingModel()
            {
                URL = banner.Data.URL,
                ImageName = banner.Data.ImageName,
                Title = banner.Data.Title,
                CreateDate=banner.Data.CreateDate
            } : null,
            Error = banner.Error,
            IsSuccess = banner.IsSuccess,
            StatusCode = banner.StatusCode
        };
        return HandleApiResult(model);
    }
}


