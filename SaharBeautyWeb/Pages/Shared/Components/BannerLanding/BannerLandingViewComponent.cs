using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Services.Banners;

namespace SaharBeautyWeb.Pages.Shared.Components.BannerLanding;

public class BannerLandingViewComponent : ViewComponent
{
    private readonly IBannerService _service;

    public BannerLandingViewComponent(IBannerService service)
    {
        _service = service;
    }

    public async Task <IViewComponentResult> InvokeAsync()
    {
        var banner = await _service.Get();

        return View(new BannerLandingModel()
        {
            Title=banner.Data.Title,
            CreateDate = banner.Data.CreateDate,
            ImageName=banner.Data.ImageName,
            URL = banner.Data.URL
        });
    }
}
public class BannerLandingModel
{
    public string?  Title { get; set; }
    public string?  URL{ get; set; }
    public string? ImageName { get; set; }
    public DateTime? CreateDate { get; set; }

}