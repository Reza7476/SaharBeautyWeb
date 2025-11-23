using SaharBeautyWeb.Models.Commons.Models;

namespace SaharBeautyWeb.Models.Entities.Treatments.Models;

public class GetAllTreatmentImagesModel
{
    public string TreatmentTitle { get; set; } = default!;
    public List<ImageDetailsModel> Images { get; set; } = new();
}
