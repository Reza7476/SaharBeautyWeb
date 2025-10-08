namespace SaharBeautyWeb.Models.Entities.Banners.Management.Models;

public class EditBannerModel
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile? Image { get; set; }
}
