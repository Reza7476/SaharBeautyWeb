using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos.Clients;
using SaharBeautyWeb.Models.Entities.Appointments.Models.Clients;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.AppointmentReview;
using SaharBeautyWeb.Services.UserPanels.Clients.Appointments;

namespace SaharBeautyWeb.Pages.UserPanels.Client.MyAppointments;

public class IndexModel : AjaxBasePageModel
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAppointmentReviewService _reviewService;


    public IndexModel(
        ErrorMessages errorMessage,
        IAppointmentService appointmentService,
        IAppointmentReviewService reviewService) : base(errorMessage)
    {
        _appointmentService = appointmentService;
        _reviewService = reviewService;
    }

    public GetAllMyAppointmentsModel ListMyAppointment { get; set; } = new();

    [BindProperty]
    public AddClientReviewModel ClientReviewModel { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public ClientAppointmentFilterModel? Filter { get; set; }
    public async Task<IActionResult> OnGet(int pageNumber = 0, int limit = 10)
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

        var result = await _appointmentService
            .GetAllClientAppointments(offset, limit, filter);

        var response = HandleApiResult(result);

        if (result.IsSuccess && result.Data != null)
        {
            ListMyAppointment.MyAppointmentModel = result.Data.Elements;
            ListMyAppointment.TotalElements = result.Data.TotalElements;
            ListMyAppointment.CurrentPage = pageNumber;
            ListMyAppointment.TotalPages = result.Data.TotalElements.ToTotalPage(limit);
        }

        ViewData["Day"] = Filter?.Day;
        ViewData["Status"] = Filter?.Status;
        ViewData["Date"] = Filter?.Date;
        return response;
    }

    public async Task<IActionResult> OnPostCancelAppointment(string id)
    {
        var result = await _appointmentService.CancelByClient(id);

        var response = HandleApiAjxResult(result);
        return response;
    }


    public PartialViewResult OnGetAddCommentPartial(string appointmentId)
    {

        ClientReviewModel = new AddClientReviewModel()
        {
            AppointmentId = appointmentId
        };

        return Partial("_AddCommentPartial", ClientReviewModel);
    }

    public async Task<IActionResult> OnPostAddClientComment()
    {

        var addComment = await _reviewService
            .AddClientReview(new AddClientReviewAppointmentDto()
            {
                AppointmentId = ClientReviewModel.AppointmentId,
                Description = ClientReviewModel.Description,
                Rate = ClientReviewModel.Rate
            });

        var response = HandleApiAjxResult(addComment);

        return response;
    }

}
