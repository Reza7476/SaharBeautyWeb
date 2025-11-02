using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos.Clients;
using SaharBeautyWeb.Models.Entities.Appointments.Models.Clients;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Clients.ClientService;

namespace SaharBeautyWeb.Pages.UserPanels.Client.MyAppointments;

public class IndexModel : AjaxBasePageModel
{

    private readonly IClientService _clientService;
    public IndexModel(
        ErrorMessages errorMessage,
        IClientService clientService) : base(errorMessage)
    {
        _clientService = clientService;
    }

    public GetAllMyAppointmentsModel ListMyAppointment { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public ClientAppointmentFilterModel? Filter { get; set; }
    public async Task<IActionResult> OnGet(int pageNumber = 0, int limit = 5)
    {
        int offset = pageNumber;
        DateOnly? dateOnly = null;
        if (!string.IsNullOrWhiteSpace(Filter?.Date))
        {
            string persianDateStr = Filter.Date.ConvertPersianNumberToEnglish();
            var date = persianDateStr.ConvertStringShamsiCalendarToGregorian();
            dateOnly = DateOnly.FromDateTime(date);
        }
        var filter = new ClientAppointmentFilterDto()
        {
            Date = dateOnly,
            Day = Filter != null ? Filter.Day : default,
            Status = Filter != null ? Filter.Status : 0,
        };

        var result = await _clientService
            .GetAllClientAppointments(offset, limit, filter);

        var response = HandleApiResult(result);

        if (result.IsSuccess && result.Data != null)
        {
            ListMyAppointment.MyAppointmentModel = result.Data.Elements;
            ListMyAppointment.TotalElements = result.Data.TotalElements;
            ListMyAppointment.CurrentPage = pageNumber;
            ListMyAppointment.TotalPages = result.Data.TotalElements.ToTotalPage(limit);
        }
        return response;
    }
}
