using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Appointments.Models;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Admin.WeeklySchedules;
using SaharBeautyWeb.Services.UserPanels.Treatments;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SaharBeautyWeb.Pages.UserPanels.Client.Appointment
{
    public class IndexModel : AjaxBasePageModel
    {
        private readonly ITreatmentUserPanelService _service;
        private readonly IWeeklyScheduleService _scheduleService;
        public IndexModel(ErrorMessages errorMessage, ITreatmentUserPanelService service, IWeeklyScheduleService scheduleService)
            : base(errorMessage)
        {
            _service = service;
            _scheduleService = scheduleService;
        }

        public List<GetAllTreatmentForAppointmentModel> AllTreatment { get; set; } = new();

        public GetTreatmentForAppointmentModel Details { get; set; } = default!;
        public List<DayInfoModel> CurrentWeekDays { get; set; } = new();
        public List<TimeSlotModel> TimeSlotModel { get; set; } = new();


        public async Task<IActionResult> OnGet()
        {
            var result = await _service.GetAllForAppointment();

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
            var result = await _service.GetDetails(id);
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

        public async Task<IActionResult> OnGetGetWeeklySchedule(DayWeek dayWeek,int duration)
        {
            var result = await _scheduleService.GetDaySchedule(dayWeek);

            if (result.IsSuccess && result.Data != null)
            {
                var slots = new List<TimeSlotModel>();
                var start = result.Data.StartTime; 
                var end = result.Data.EndTime;     
                var currentTime = start;
                var now = DateTime.Now;
                int todayDayOfWeek = ((int)now.DayOfWeek)+1; // 0 = Sunday, 1 = Monday ...
                                                         // اگر روز انتخاب شده همان روز هفته امروز بود
                bool isTodaySelected = ((int)dayWeek == todayDayOfWeek);

                while (currentTime < end)
                {
                    var nextTime = currentTime.AddMinutes(duration);

                    if(isTodaySelected && currentTime <= now)
                    {
                        currentTime = nextTime;
                        continue;
                    }

                    if (nextTime > end)
                        nextTime = end;

                    slots.Add(new TimeSlotModel
                    {
                        Start = currentTime,
                        End = nextTime
                    });

                    currentTime = nextTime; 
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
            var today = DateTime.Now;

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
                var date = today.AddDays(i);
                var year = persianCalendar.GetYear(date);
                var month = persianCalendar.GetMonth(date);
                var day = persianCalendar.GetDayOfMonth(date);

                string persianDate = $"{day} {GetPersianMonthName(month)} {year}";

                CurrentWeekDays.Add(new DayInfoModel
                {
                    PersianDay = days[(dayOfWeek + i) % 7],
                    PersianDate = persianDate,
                    Day = (DayWeek)((dayOfWeek + i) % 7) 
                });
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
