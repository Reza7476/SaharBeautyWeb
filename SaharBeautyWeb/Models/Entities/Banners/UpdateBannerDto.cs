namespace SaharBeautyWeb.Models.Entities.Banners;

public class UpdateBannerDto
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public IFormFile? Image { get; set; }
}
