namespace SaharBeautyWeb.Models.Entities.Banners;

public class AddBannerModel
{
    public required string Title { get; set; }
    public required IFormFile Image { get; set; }
}
