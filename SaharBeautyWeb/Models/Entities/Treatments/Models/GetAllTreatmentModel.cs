using SaharBeautyWeb.Models.Entities.Treatments.Dtos;

namespace SaharBeautyWeb.Models.Entities.Treatments.Models;

public class GetAllTreatmentModel
{
    public List<GetTreatmentDto> Treatments { get; set; } = new();
    public int TotalElements { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
