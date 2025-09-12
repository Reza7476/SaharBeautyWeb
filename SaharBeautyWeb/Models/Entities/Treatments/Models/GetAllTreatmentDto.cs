using SaharBeautyWeb.Models.Commons;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Treatments.Dtos;

public class GetAllTreatmentDto
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public MediaDto? Media { get; set; }
}


