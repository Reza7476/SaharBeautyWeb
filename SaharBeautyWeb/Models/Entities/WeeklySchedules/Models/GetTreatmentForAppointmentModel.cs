using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;

public class GetTreatmentForAppointmentModel
{
    public string Description { get; set; } = default!;
    public string Title { get; set; } = default!;
    public ImageDetailsDto Image { get; set; } = default!;
}
