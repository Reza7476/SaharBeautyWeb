using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Appointments.Models;

public class AddAdminAppointmentModel
{
    [Required(ErrorMessage = "لطفا خدمت مورد نظر را انتخاب کنید")]
    public long TreatmentId { get; set; }

    [Required(ErrorMessage = "لطفا روز مورد نظر را انتخاب کنید")]
    public DateOnly? DateOnly { get; set; }

    [Required(ErrorMessage = "لطفا زمان مورد نظر را انتخاب کنید")]
    public TimeOnly? TimeOnly { get; set; }

    [Required]
    public DayWeek DayWeek { get; set; }

    [Required]
    public string ClientId { get; set; } = default!;
}
