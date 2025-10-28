using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Models;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Admin.WeeklySchedules;
using SaharBeautyWeb.Services.UserPanels.Clients.Appointments;
using SaharBeautyWeb.Services.UserPanels.Treatments;
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
                    success=false,
                    statusCode=400,
                    error=appointmentError
                });
            }
            var result = await _appointmentService.Add(new AddAppointmentDto()
            {
                Duration =          AppointmentModel.Duration,
                TreatmentId =       AppointmentModel.TreatmentId,
                AppointmentDate =  AppointmentModel.DateOnly!.Value
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

        public async Task<IActionResult> OnGetGetWeeklySchedule(DayWeek dayWeek, int duration, DateTime date)
        {
            var result = await _scheduleService.GetDaySchedule(dayWeek);
            var booked = await _appointmentService.GetBookedByDate(date);


            if ((result.IsSuccess && result.Data != null)&&booked.IsSuccess)
            {
                var slots = new List<TimeSlotModel>();
                var start = result.Data.StartTime;
                var end = result.Data.EndTime;
                var currentTime = start;
                var now = DateTime.Now;
                int todayDayOfWeek = ((int)now.DayOfWeek) + 1; // 0 = Sunday, 1 = Monday ...
                                                               // اگر روز انتخاب شده همان روز هفته امروز بود
                bool isTodaySelected = ((int)dayWeek == todayDayOfWeek);

                while (currentTime < end)
                {
                    var nextTime = currentTime.AddMinutes(duration);

                    if (isTodaySelected && currentTime <= now)
                    {
                        currentTime = nextTime;
                        continue;
                    }

                    if (nextTime > end)
                        nextTime = end;

                    slots.Add(new TimeSlotModel
                    {
                        Start = TimeOnly.FromDateTime(currentTime),
                        End = TimeOnly.FromDateTime( nextTime),
                        IsActive=true
                    });

                    currentTime = nextTime;
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

                return HandleApiAjaxPartialResult(
                    result,
                    data => slots,
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
                var year = persianCalendar.GetYear(dateUtc);
                var month = persianCalendar.GetMonth(dateUtc);
                var day = persianCalendar.GetDayOfMonth(dateUtc);

                string persianDate = $"{day} {GetPersianMonthName(month)} {year}";

                CurrentWeekDays.Add(new DayInfoModel
                {
                    PersianDay = days[(dayOfWeek + i) % 7],
                    PersianDate = persianDate,
                    Day = (DayWeek)((dayOfWeek + i) % 7),
                    Date = DateOnly.FromDateTime(dateUtc)
                });
            }
            var a = string.Empty;
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
