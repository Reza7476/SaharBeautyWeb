using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Models;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Admin.Treatments;
using SaharBeautyWeb.Services.UserPanels.Clients.Appointments;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.Appointments;

public class IndexModel : AjaxBasePageModel
{

    private readonly IAppointmentService _appointmentService;
    private readonly ITreatmentUserPanelService _treatmentService;
    public IndexModel(
        ErrorMessages errorMessage,
        IAppointmentService appointmentService
,
        ITreatmentUserPanelService treatmentService) : base(errorMessage)
    {
        _appointmentService = appointmentService;
        _treatmentService = treatmentService;
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public AdminAppointmentFilterModel Filter { get; set; } = new();

    public GetAllAppointmentsModel ListAppointments { get; set; } = new();
    public List<TreatmentTitleModel> TreatmentTitles { get; set; } = new();

    public async Task<IActionResult> OnGet(int pageNumber = 0, int limit = 5)
    {

        if (!string.IsNullOrWhiteSpace(Search))
        {
            Search = Search.RemoveCountryCodeFromPhoneNumber();
        }
        int offset = pageNumber;
        DateOnly? dateOnly = null;
        if (!string.IsNullOrWhiteSpace(Filter?.Date))
        {
            string persianDateString = Filter.Date.ConvertPersianNumberToEnglish();
            var date = persianDateString.ConvertStringShamsiCalendarToGregorian();
            dateOnly = DateOnly.FromDateTime(date);
        }

        var filter = new AdminAppointmentFilterDto()
        {
            Date = dateOnly,
            Day = Filter != null ? Filter.Day : default,
            Status = Filter != null ? Filter.Status : default,
            TreatmentTitle = Filter != null ? Filter.TreatmentTitle : default
        };

        var treatmentTitle = await _treatmentService.GetTitlesForAdmin();
        if (treatmentTitle.IsSuccess && treatmentTitle.Data != null)
        {
            TreatmentTitles = treatmentTitle.Data.Select(_ => new TreatmentTitleModel()
            {
                Title = _.Title
            }).ToList();
        }


        var result = await _appointmentService.GetAllAdminAppointments(offset, limit, filter, Search);
        var response = HandleApiResult(result);
        if (result.IsSuccess && result.Data != null)
        {
            ListAppointments.AllAppointmentModel = result.Data.Elements;
            ListAppointments.TotalElements = result.Data.TotalElements;
            ListAppointments.CurrentPage = pageNumber;
            ListAppointments.TotalPages = result.Data.TotalElements.ToTotalPage(limit);
        }
        ViewData["Search"] = Search;
        ViewData["Day"] = Filter?.Day;
        ViewData["Status"] = Filter?.Status;
        ViewData["TreatmentTitle"] = Filter?.TreatmentTitle;
        ViewData["Date"] = Filter?.Date;
        return response;
    }
}
