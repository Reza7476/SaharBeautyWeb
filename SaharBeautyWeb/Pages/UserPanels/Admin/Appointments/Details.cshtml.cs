using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Appointments.Enums;
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

    public GetAppointmentDetailsModel? AppointmentDetails { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }


    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Filter_Day { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Filter_Status { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Filter_Date { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Filter_TreatmentTitle { get; set; }

    public async Task<IActionResult> OnGet(
        string id,
        int pageNumber,
        string? search,
        string? filter_day,
        string? filter_status,
        string? filter_date,
        string? filter_treatmentTitle)
    {

       
        var result = await _appointmentService.GetDetails(id);
        if (result.IsSuccess)
        {
            if (result.Data != null)
            {
                AppointmentDetails = new GetAppointmentDetailsModel()
                {
                    ClientLastName = result.Data.ClientLastName,
                    ClientMobile = result.Data.ClientMobile,
                    ClientName = result.Data.ClientName,
                    Date = result.Data.Date.ConvertDateOnlyToPersian(),
                    Day = result.Data.Day.ConvertDayWeekToPersianDay(),
                    EndTime = result.Data.EndTime,
                    StatusString = result.Data.Status.ConvertAppointmentStatusToString(),
                    Status = result.Data.Status,
                    StartTime = result.Data.StartTime,
                    Duration = result.Data.Duration,
                    TreatmentTitle = result.Data.TreatmentTitle,
                    Price = result.Data.Price.ConvertDecimalNumberToString(),
                    Id = id
                };
            }
        }

        PageNumber = pageNumber;
        Search = search;
        Filter_Day = filter_day;
        Filter_Status = filter_status;
        Filter_Date = filter_date;
        Filter_TreatmentTitle = filter_treatmentTitle;
        var query = new Dictionary<string, string?>
            {
                {"PageNumber", pageNumber.ToString()},
                {"Search", search},
                {"Day", filter_day},
                {"Status", filter_status},
                {"Date", filter_date},
                {"TreatmentTitle", filter_treatmentTitle}
            };
        var queryString = string.Join("&",
                 query.Where(x => !string.IsNullOrWhiteSpace(x.Value))
                      .Select(x => $"{x.Key}={x.Value}")
             );
        var url = $"/UserPanels/Admin/Appointments/Index?{queryString}";


        TempData["ReturnUrl"] = url;
        var response = HandleApiResult(result);
        return response;
    }

    public async Task<IActionResult> OnPostChangeStatus(string id, AppointmentStatus status)
    {
        var result = await _appointmentService.ChangeStatus(id, status);
        var response = HandleApiAjxResult(result);
        return response;
    }
}
