namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Treatments.Dtos;


public class TreatmentElementDto
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public MediaDto? Media { get; set; }
}
