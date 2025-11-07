using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Models;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Admin.Treatments;
using SaharBeautyWeb.Services.UserPanels.Admin.WeeklySchedules;
using SaharBeautyWeb.Services.UserPanels.Clients.Appointments;
using System.Globalization;

namespace SaharBeautyWeb.Pages.UserPanels.Client.Appointment
{
    public class IndexModel : AjaxBasePageModel
    {
        private readonly ITreatmentUserPanelService _treatmentService;
        private readonly IWeeklyScheduleService _scheduleService;
        private readonly IAppointmentService _appointmentService;

        public IndexModel(
            ErrorMessages errorMessage,
            ITreatmentUserPanelService service,
            IWeeklyScheduleService scheduleService,
            IAppointmentService appointmentService)
            : base(errorMessage)
        {
            _treatmentService = service;
            _scheduleService = scheduleService;
            _appointmentService = appointmentService;
        }

        public List<GetAllTreatmentForAppointmentModel> AllTreatment { get; set; } = new();

        public GetTreatmentForAppointmentModel Details { get; set; } = default!;
        public List<DayInfoModel> CurrentWeekDays { get; set; } = new();
        public List<TimeSlotModel> TimeSlotModel { get; set; } = new();
        public GetDayScheduleModel DaySchedule { get; set; } = new();
        [BindProperty]
        public AddAppointmentModel AppointmentModel { get; set; } = default!;
        public bool HasSubmitted { get; set; } = false;

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
            var result = await _appointmentService.Add(new AddAppointmentDto()
            {
                Duration = HttpContext.Session.GetInt32("SelectedTreatmentDuration") ?? 0,
                TreatmentId = AppointmentModel.TreatmentId,
                DayWeek = AppointmentModel.DayWeek,
                AppointmentDate = AppointmentModel.DateOnly!.Value
                                    .ToDateTime(AppointmentModel.TimeOnly!.Value)
            });

            return HandleApiAjxResult(result);
        }



        public async Task<IActionResult> OnGet()
        {
            var result = await _treatmentService.GetAllForAppointment();

            var apiResult = new ApiResultDto<List<GetAllTreatmentForAppointmentModel>>()
            {
                Error = result.Error,
                IsSuccess = result.IsSuccess,
                StatusCode = result.StatusCode,
                Data = result.Data!.Select(treatment => new GetAllTreatmentForAppointmentModel()
                {
                    Id = treatment.Id,
                    Title = treatment.Title
                }).ToList()
            };

            var response = HandleApiResult(apiResult);
            if (response is PageResult)
            {
                AllTreatment = apiResult.Data!;
                GenerateWeekDays();
            }

            return response;
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

        public async Task<IActionResult> OnGetGetWeeklySchedule(DayWeek dayWeek,  DateTime date)
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
            return HandleApiAjaxPartialResult(result, data => new List<TimeSlotModel>(), "_timeSlotList");

        }

        private void GenerateWeekDays()
        {
            var persianCalendar = new PersianCalendar();
            var today = DateTime.UtcNow;

            string[] days = {
                "شنبه",
                "یک‌شنبه",
                "دوشنبه",
                "سه‌شنبه",
                "چهارشنبه",
                "پنج‌شنبه",
                "جمعه",
            };

            int dayOfWeek = ((int)persianCalendar.GetDayOfWeek(today) + 1) % 7;

            for (int i = 0; i < 7; i++)
            {
                var dateUtc = today.AddDays(i);
                var currentDayOfWeek = dateUtc.DayOfWeek;
                var year = persianCalendar.GetYear(dateUtc);
                var month = persianCalendar.GetMonth(dateUtc);
                var day = persianCalendar.GetDayOfMonth(dateUtc);
                string persianDate = $"{day} {GetPersianMonthName(month)} {year}";

                CurrentWeekDays.Add(new DayInfoModel
                {
                    PersianDay = days[(dayOfWeek + i) % 7],
                    PersianDate = persianDate,
                    Day = GetProjectDayWeek(currentDayOfWeek),
                    Date = DateOnly.FromDateTime(dateUtc)
                });
            }
        }

        private DayWeek GetProjectDayWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Saturday: return DayWeek.Saturday;
                case DayOfWeek.Sunday: return DayWeek.Sunday;
                case DayOfWeek.Monday: return DayWeek.Monday;
                case DayOfWeek.Tuesday: return DayWeek.Tuesday;
                case DayOfWeek.Wednesday: return DayWeek.Wednesday;
                case DayOfWeek.Thursday: return DayWeek.Thursday;
                case DayOfWeek.Friday: return DayWeek.Friday;
                default: return DayWeek.None;
            }
        }

        private DayOfWeek GetSystemDayWeek(DayWeek dayweek)
        {
            switch (dayweek)
            {
                case DayWeek.Saturday: return DayOfWeek.Saturday;
                case DayWeek.Sunday: return DayOfWeek.Sunday;
                case DayWeek.Monday: return DayOfWeek.Monday;
                case DayWeek.Tuesday: return DayOfWeek.Tuesday;
                case DayWeek.Wednesday: return DayOfWeek.Wednesday;
                case DayWeek.Thursday: return DayOfWeek.Thursday;
                case DayWeek.Friday: return DayOfWeek.Friday;
                default: throw new Exception();
            }
        }


        private string GetPersianMonthName(int month)
        {
            string[] months = {
                "فروردین",
                "اردیبهشت",
                "خرداد",
                "تیر",
                "مرداد",
                "شهریور",
                "مهر",
                "آبان",
                "آذر",
                "دی",
                "بهمن",
                "اسفند"
            };
            return months[month - 1];
        }
    }
}
