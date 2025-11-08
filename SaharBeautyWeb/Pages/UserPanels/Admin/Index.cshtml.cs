using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Appointments.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Clients.Appointments;

namespace SaharBeautyWeb.Pages.UserPanels.Admin;

public class IndexModel : AjaxBasePageModel
{

    private readonly IAppointmentService _appointmentService;

    public IndexModel(
        ErrorMessages errorMessage,
        IAppointmentService appointmentService) : base(errorMessage)
    {
        _appointmentService = appointmentService;
    }
    public List<AppointmentPerDayModel> AppointmentsPerDay { get; set; } = new();

    public async Task<IActionResult> OnGet()
    {
        var result = await _appointmentService.GetAppointmentPerDayForChart();
        AppointmentsPerDay = result.Data!.Select(_ => new AppointmentPerDayModel()
        {
            DayWeek = StringExtension.ConvertDayWeekToPersianDay(_.DayWeek),
            Count = _.Count
        }).ToList();
        

        return null;
    }
}
