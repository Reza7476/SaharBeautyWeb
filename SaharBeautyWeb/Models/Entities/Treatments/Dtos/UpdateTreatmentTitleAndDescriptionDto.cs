namespace SaharBeautyWeb.Models.Entities.Treatments.Dtos;

public class UpdateTreatmentTitleAndDescriptionDto
{
    public required string  Description { get; set; }
    public required string  Title { get; set; }
    public long Id { get; set; }
}
