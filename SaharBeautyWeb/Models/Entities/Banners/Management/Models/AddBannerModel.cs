using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Banners.Management.Models;

public class AddBannerModel
{
    [Required(ErrorMessage = "عنوان اجباری است")]
    public string Title { get; set; }

    [Required(ErrorMessage = "تصویر اجباری است")]
    public IFormFile Image { get; set; }
}