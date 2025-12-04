using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Commons.Models;
using SaharBeautyWeb.Models.Entities.Appointments.Models;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Admin.Treatments;
using SaharBeautyWeb.Services.UserPanels.Admin.WeeklySchedules;
using SaharBeautyWeb.Services.UserPanels.Clients.Appointments;
using SaharBeautyWeb.Services.UserPanels.UserFCMTokens;

namespace SaharBeautyWeb.Pages.UserPanels.Admin;

public class IndexModel : AjaxBasePageModel
{

    private readonly IAppointmentService _appointmentService;
    private readonly IWeeklyScheduleService _weeklySchedule;
    private readonly ITreatmentUserPanelService _treatmentService;
    private readonly IUserFCMTokenService _userFCMTokenService;


    public IndexModel(
        ErrorMessages errorMessage,
        IAppointmentService appointmentService,
        IWeeklyScheduleService weeklySchedule,
        ITreatmentUserPanelService treatmentService,
        IUserFCMTokenService userFCMTokenService) : base(errorMessage)
    {
        _appointmentService = appointmentService;
        _weeklySchedule = weeklySchedule;
        _treatmentService = treatmentService;
        _userFCMTokenService = userFCMTokenService;
    }
    public List<AppointmentPerDayModel> AppointmentsPerDay { get; set; } = new();

    public List<GetWeeklyScheduleDashboardModel> WeeklySchedule { get; set; } = new();
    public List<GetPopularTreatmentsModel> PopularTreatments { get; set; } = new();

    public GetAdminDashboardSummaryModel? DashboardSummary { get; set; }

    public List<GetAdminDashboardNewAppointmentsModel> NewAppointmentModel { get; set; } = new();
    public async Task<IActionResult> OnGet()
    {

        var appointmentPerDay = await _appointmentService.GetAppointmentPerDayForChart();
        if (appointmentPerDay.IsSuccess && appointmentPerDay.Data != null)
        {
            AppointmentsPerDay = appointmentPerDay.Data!.Select(_ => new AppointmentPerDayModel()
            {
                DayWeek = StringExtension.ConvertDayWeekToPersianDay(_.DayWeek),
                Count = _.Count
            }).ToList();

        }
        else
        {
            var response = HandleApiResult(appointmentPerDay);
        }
      
        var weeklySchedule = await _weeklySchedule.GetSchedules();
        if (weeklySchedule.IsSuccess && weeklySchedule.Data != null)
        {
            WeeklySchedule = weeklySchedule.Data.Select(_ => new GetWeeklyScheduleDashboardModel()
            {
                Day = _.DayOfWeek.ConvertDayWeekToPersianDay(),
                Start = TimeOnly.FromDateTime(_.StartTime).ToString(),
                End = TimeOnly.FromDateTime(_.EndTime).ToString(),
                IsActive = _.IsActive

            }).ToList();
        }
        else
        {
            var response= HandleApiResult(weeklySchedule);
        }

        var popularTreatments = await _treatmentService.GetPopularTreatments();
        if (popularTreatments.IsSuccess && popularTreatments.Data != null)
        {
            PopularTreatments = popularTreatments.Data.Select(_ => new GetPopularTreatmentsModel()
            {
                AppointmentCount = _.AppointmentCount,
                Id = _.Id,
                Title = _.Title,
                Image = _.Image != null ? new ImageDetailsDto()
                {
                    Extension = _.Image.Extension,
                    ImageName = _.Image.ImageName,
                    UniqueName = _.Image.UniqueName,
                    Url = _.Image.Url
                } : null,
            }).ToList();
        }
        else
        {
            var response=HandleApiResult(popularTreatments);
        }


        var dashboardSummary = await _appointmentService.GetAdminDashboardSummary();
        if (dashboardSummary.IsSuccess && dashboardSummary.Data != null)
        {
            DashboardSummary = new GetAdminDashboardSummaryModel()
            {
                TodayAppointments = dashboardSummary.Data.TodayAppointments,
                TotalClients = dashboardSummary.Data.TotalClients,
                TotalTreatments = dashboardSummary.Data.TotalTreatments,
            };
        }
        else
        {
            var response = HandleApiResult(dashboardSummary);
        }

        var newAppoints = await _appointmentService.GetNewAppointmentDashboard();

        if (newAppoints.IsSuccess && newAppoints.Data != null)
        {
            NewAppointmentModel = newAppoints.Data.Select(_ => new GetAdminDashboardNewAppointmentsModel()
            {
                ClientLastName = _.ClientLastName,
                ClientName = _.ClientName,
                Date = DateTimeExtension.ConvertDateOnlyToPersian(_.Date),
                DayWeek = StringExtension.ConvertDayWeekToPersianDay(_.DayWeek),
                Mobile = _.Mobile,
                StatusString = StringExtension.ConvertAppointmentStatusToString(_.Status),
                TreatmentTitle = _.TreatmentTitle,
                Status=_.Status
            }).ToList();
        }
        else
        {
            var response = HandleApiResult(newAppoints);
        }

        return Page();
    }
}

