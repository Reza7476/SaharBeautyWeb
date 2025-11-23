using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;
using SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;
using SaharBeautyWeb.Services.Landing.Treatments;
using SaharBeautyWeb.Services.UserPanels.Admin.Treatments;

namespace SaharBeautyWeb.Pages.Shared.Components.LandingGallery;

public class LandingGalleryViewComponent : LandingBaseViewComponent
{

    private readonly ITreatmentUserPanelService _treatmentService;
    public LandingGalleryViewComponent(
        ErrorMessages errorMessage,
        ITreatmentUserPanelService treatmentService) : base(errorMessage)
    {
        _treatmentService = treatmentService;
    }


    public GetTreatmentGalleryImageModel GalleryModel { get; set; } = new(); 
    

    public async Task<IViewComponentResult> InvokeAsync(int pageNumber = 0)
    {
        int offset = pageNumber;
        var limit = 10;
        var result = await _treatmentService.GetGalleryImage(offset,limit);
        if (result.IsSuccess && result.Data != null)
        {
            GalleryModel.AllTreatmentImages = result.Data.Elements;
            GalleryModel.TotalElements = result.Data.TotalElements;
            GalleryModel.CurrentPage = pageNumber;
            GalleryModel.TotalPages = result.Data.TotalElements.ToTotalPage(limit);
        }
        return View(GalleryModel);
    }
}
