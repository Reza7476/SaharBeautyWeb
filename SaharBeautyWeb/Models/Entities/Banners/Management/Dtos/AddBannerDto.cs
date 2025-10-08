namespace SaharBeautyWeb.Models.Entities.Banners.Management.Dtos;

public class AddBannerDto
{
    public required string Title { get; set; }

    public required IFormFile Image { get; set; }
}
