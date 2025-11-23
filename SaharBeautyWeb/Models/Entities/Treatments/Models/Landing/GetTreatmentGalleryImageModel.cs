namespace SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;

public class GetTreatmentGalleryImageModel
{
    public List<GetAllTreatmentImagesModel> AllTreatmentImages { get; set; } = new();
    public int TotalElements { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
