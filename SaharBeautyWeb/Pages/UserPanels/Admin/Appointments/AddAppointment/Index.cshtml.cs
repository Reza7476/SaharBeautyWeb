using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Models;
using SaharBeautyWeb.Models.Entities.Clients.Models;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Admin.Treatments;
using SaharBeautyWeb.Services.UserPanels.Admin.WeeklySchedules;
using SaharBeautyWeb.Services.UserPanels.Clients.Appointments;
using SaharBeautyWeb.Services.UserPanels.Clients.ClientServices;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.Appointments.AddAppointment;

public class IndexModel : AjaxBasePageModel
{
    private readonly IClientService _clientService;
    private readonly ITreatmentUserPanelService _treatmentService;
    private readonly IWeeklyScheduleService _scheduleService;
    private readonly IAppointmentService _appointmentService;

    public IndexModel(
        ErrorMessages errorMessage,
        IClientService clientService,
        ITreatmentUserPanelService treatmentService,
        IWeeklyScheduleService scheduleService,
        IAppointmentService appointmentService) : base(errorMessage)
    {
        _clientService = clientService;
        _treatmentService = treatmentService;
        _scheduleService = scheduleService;
        _appointmentService = appointmentService;
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }
    public GetTreatmentForAppointmentModel Details { get; set; } = default!;
    public List<GetAllClientsForAddAppointmentModel> AllClients { get; set; } = new();
    public List<GetAllTreatmentForAppointmentModel> AllTreatments { get; set; } = new();
    public GetDayScheduleModel DaySchedule { get; set; } = new();

    public List<DayInfoModel> DaysOfWeek { set; get; } = new();

    [BindProperty]
    public AddAdminAppointmentModel AppointmentModel { get; set; } = new();

    public async Task<IActionResult> OnGet()
    {

        var client = await _clientService.GetAllForAppointment(Search);
        var clientResponse = HandleApiResult(client);
        if (client.IsSuccess && client.Data != null)
        {
            AllClients = client.Data.Select(_ => new GetAllClientsForAddAppointmentModel()
            {
                Id = _.Id,
                LastName = _.LastName,
                MobileNumber = _.MobileNumber,
                Name = _.Name,
                Profile = _.Profile != null ? new ImageDetailsDto
                {
                    Extension = _.Profile.Extension,
                    ImageName = _.Profile.ImageName,
                    UniqueName = _.Profile.UniqueName,
                    Url = _.Profile.Url
                } : null
            }).ToList();
        }

        var treatments = await _treatmentService.GetAllForAppointment();
        var treatmentResponse = HandleApiResult(treatments);
        if (treatments.IsSuccess && treatments.Data != null)
        {
            AllTreatments = treatments.Data.Select(_ => new GetAllTreatmentForAppointmentModel()
            {
                Id = _.Id,
                Title = _.Title,
            }).ToList();
        }

        if (clientResponse is PageResult)
        {
            DaysOfWeek = DateTimeExtension.GeneratePersianWeekDays(31);
        }

        return clientResponse;
    }


    public async Task<IActionResult> OnGetGetTreatmentDetails(int id)
    {
        var result = await _treatmentService.GetDetails(id);
        if (result.IsSuccess && result.Data != null && result.Data.Duration != 0)
        {
            HttpContext.Session.SetInt32("SelectedTreatmentDuration", result.Data.Duration);
        }
        return HandleApiAjaxPartialResult(
            result,
            data => new GetTreatmentForAppointmentModel()
            {
                Description = data.Description,
                Title = data.Title,
                Image = data.Image,
                Duration = data.Duration,
                Id = id

            }, "_treatmentDetails");
    }
    public async Task<IActionResult> OnGetGetWeeklySchedule(DayWeek dayWeek, DateTime date)
    {
        var result = await _scheduleService.GetDaySchedule(dayWeek);
        var booked = await _appointmentService.GetBookedByDate(date);

        var duration = HttpContext.Session.GetInt32("SelectedTreatmentDuration") ?? 0;
        if ((result.IsSuccess && result.Data != null) && (booked.IsSuccess && booked.Data != null))
        {
            var slots = new List<TimeSlotModel>();
            if (result.Data.IsActive)
            {
                DaySchedule.IsActive = true;

                var now = DateTime.Now;
                TimeSpan totalDate = result.Data.EndTime - result.Data.StartTime;
                int validCount = (int)totalDate.TotalMinutes / duration;

                var start = TimeOnly.FromDateTime(result.Data.StartTime);
                var end = TimeOnly.FromDateTime(result.Data.EndTime);

                var todayDayOfWeekSystem = now.DayOfWeek;
                var selectedDay = DateTimeExtension.GetSystemDayWeek(dayWeek);
                bool isTodaySelected = (todayDayOfWeekSystem == selectedDay); // اگر روز انتخاب شده همان روز هفته امروز بود

                while (start < end)
                {
                    if (slots.Count == validCount) break;
                    var nextTime = start.AddMinutes(duration);

                    if (isTodaySelected && start <= (TimeOnly.FromDateTime(now)))
                    {
                        start = nextTime;
                        validCount = validCount - 1;
                        continue;
                    }

                    if (nextTime > end)
                        nextTime = end;

                    slots.Add(new TimeSlotModel
                    {
                        Start = start,
                        End = nextTime,
                        IsActive = true,
                    });
                    start = nextTime;
                }

                if (booked.IsSuccess && booked.Data != null)
                {
                    foreach (var slot in slots)
                    {
                        foreach (var b in booked.Data)
                        {
                            // بررسی تداخل زمانی
                            if (slot.Start < b.EndDate && b.StartDate < slot.End)
                            {
                                slot.IsActive = false;
                                break;
                            }
                        }
                    }
                }
                DaySchedule.TimeSlots = slots;
            }
            else
            {
                DaySchedule.IsActive = false;
                DaySchedule.Description = "خدماتی در این روز ارایه نمیگردد";
            }
            return HandleApiAjaxPartialResult(
                result,
                data => DaySchedule,
                "_timeSlotList"
            );
        }

        // اگر موفق نبود، پیام خطا برمی‌گردد
        return HandleApiAjaxPartialResult(result, data => DaySchedule, "_timeSlotList");

    }



    public async Task<IActionResult> OnPostReserve()
    {
        var appointmentError = ModelState
               .Where(x => x.Key.StartsWith("AppointmentModel.") &&
               x.Value?.Errors.Count > 0)
               .ToDictionary(
                 kvp => kvp.Key,
                 kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray());
        if (appointmentError.Any())
        {
            return new JsonResult(new
            {
                success = false,
                statusCode = 400,
                error = appointmentError
            });
        }
        var result = await _appointmentService.AddAdminAppointment(new AddAdminAppointmentDto()
        {
            ClientId=AppointmentModel.ClientId,
            DayWeek=AppointmentModel.DayWeek,
            Duration= HttpContext.Session.GetInt32("SelectedTreatmentDuration")??0,
            TreatmentId=AppointmentModel.TreatmentId,
            AppointmentDate = AppointmentModel.DateOnly!.Value
                                    .ToDateTime(AppointmentModel.TimeOnly!.Value)
        });
        return HandleApiAjxResult(result);
    }
}
