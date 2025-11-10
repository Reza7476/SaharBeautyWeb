using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons;
using SaharBeautyWeb.Models.Commons.Models;
using SaharBeautyWeb.Models.Entities.Users.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Clients.Appointments;
using SaharBeautyWeb.Services.UserPanels.Users;
using System.Runtime.CompilerServices;

namespace SaharBeautyWeb.Pages.UserPanels.Client;

public class IndexModel : AjaxBasePageModel
{

    private readonly IUserService _userService;
    private readonly IAppointmentService _appointmentService;

    public IndexModel(
        ErrorMessages errorMessage,
        IUserService userService,
        IAppointmentService appointmentService) : base(errorMessage)
    {
        _userService = userService;
        _appointmentService = appointmentService;
    }
    public UserInfoModel? UserInfo { get; set; }

    public DashboardClientSummaryModel? SummaryModel { get; set; }
    public async Task<IActionResult> OnGet()
    {
        var userInfo = await _userService.GetUserInfo();
        if (userInfo.IsSuccess && userInfo.Data != null)
        {
            UserInfo = new UserInfoModel()
            {
                Avatar = userInfo.Data.Avatar != null ? new ImageDetailsModel()
                {
                    Extension = userInfo.Data.Avatar.Extension,
                    ImageName = userInfo.Data.Avatar.ImageName,
                    UniqueName = userInfo.Data.Avatar.UniqueName,
                    Url = userInfo.Data.Avatar.Url
                } : null,
                BirthDate = userInfo.Data.BirthDate != null ? userInfo.Data.BirthDate.Value.ConvertGregorianDateToShamsi() : " ",
                CreationDate = userInfo.Data.CreationDate.ConvertGregorianDateToShamsi(),
                Email = userInfo.Data.Email,
                Id = userInfo.Data.Id,
                IsActive = userInfo.Data.IsActive,
                LastName = userInfo.Data.LastName,
                Mobile = userInfo.Data.Mobile,
                Name = userInfo.Data.Name,
                RoleNames = userInfo.Data.RoleNames,
                UserName = userInfo.Data.UserName
            };
        }
        else
        {
            HandleApiResult(userInfo);
        }
        


        var summary = await _appointmentService.GetDashboardClientSummary();
        if (summary.IsSuccess && summary != null && summary.Data != null)
        {
            SummaryModel = new();
            SummaryModel.FutureAppointments = summary.Data.FutureAppointments.Select(_ => new DashboardClientAppointmentModel()
            {
                Date = DateTimeExtension.ConvertDateOnlyToPersian(_.Date),
                Day = StringExtension.ConvertDayWeekToPersianDay(_.Day),
                Start = _.Start.ToString(),
                Status = StringExtension.ConvertAppointmentStatusToString(_.Status),
            }).ToList();
            SummaryModel.FormerAppointments = summary.Data.FormerAppointments.Select(_ => new DashboardClientAppointmentModel()
            {
                Date = DateTimeExtension.ConvertDateOnlyToPersian(_.Date),
                Day = StringExtension.ConvertDayWeekToPersianDay(_.Day),
                Start = _.Start.ToString(),
                Status = StringExtension.ConvertAppointmentStatusToString(_.Status),
            }).ToList();

        }else
        {
            HandleApiResult(summary);
        }
        return Page();
    }
}
