using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Appointments.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Clients.Appointments;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.Appointments;

public class DetailsModel : AjaxBasePageModel
{

    private readonly IAppointmentService _appointmentService;
    public DetailsModel(
        ErrorMessages errorMessage,
        IAppointmentService appointmentService) : base(errorMessage)
    {
        _appointmentService = appointmentService;
    }

    public GetAppointmentDetailsModel AppointmentDetails { get; set; } = new();

    public async Task<IActionResult> OnGet(string id)
    {
        var result = await _appointmentService.GetDetails(id);
        var response = HandleApiResult(result);
        if (result.IsSuccess)
        {
            if(result.Data != null)
            {
                AppointmentDetails = new GetAppointmentDetailsModel()
                {
                    ClientLastName = result.Data.ClientLastName,
                    ClientMobile = result.Data.ClientMobile,
                    ClientName = result.Data.ClientName,
                    Date = result.Data.Date.ConvertDateOnlyToPersian(),
                    Day = result.Data.Day.ConvertDayWeekToPersianDay(),
                    EndTime = result.Data.EndTime,
                    Status = result.Data.Status.ConvertAppointmentStatusToString(),
                    StartTime= result.Data.StartTime,
                    Duration=result.Data.Duration,
                    TreatmentTitle= result.Data.TreatmentTitle,
                    Price=result.Data.Price.ConvertDecimalNumberToString(),
                };
            }
        }

        return response;
    }
}
