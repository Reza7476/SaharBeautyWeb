using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.Banners.Landing;

public class BannerLandingModel : ViewComponentErrorDto
{
    public string? Title { get; set; }
    public string? URL { get; set; }
    public string? ImageName { get; set; }
    public DateTime? CreateDate { get; set; }
}