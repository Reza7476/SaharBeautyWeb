using SaharBeautyWeb.Models.Entities.Treatments.Dtos;

namespace SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;

public class GetAllTreatmentForLandingModel
{
    public List<GetTreatmentDto> Treatments { get; set; } = new();
    public int TotalElements { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
