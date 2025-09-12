using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Treatments.Dtos;


public class AddTreatmentModel
{
    [Required]
    public string Title { get; set; } = default!;
    
    
    [Required]
    public string Description { get; set; }=default!;

    [Required]
    public IFormFile Image { get; set; } = default!;
}
