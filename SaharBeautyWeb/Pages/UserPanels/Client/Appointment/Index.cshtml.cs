using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Treatments;
using System.Globalization;

namespace SaharBeautyWeb.Pages.UserPanels.Client.Appointment
{
    public class IndexModel : AjaxBasePageModel
    {
        private readonly ITreatmentUserPanelService _service;

        public IndexModel(ErrorMessages errorMessage, ITreatmentUserPanelService service)
            : base(errorMessage)
        {
            _service = service;
        }

        public List<GetAllTreatmentForAppointmentModel> AllTreatment { get; set; } = new();

        public GetTreatmentForAppointmentModel Details { get; set; } = default!;
        public List<DayInfoModel> CurrentWeekDays { get; set; } = new();

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
                    Description=data.Description,
                    Title=data.Title,
                    Image=data.Image,
                    
                }, "_treatmentDetails");

        }



        private void GenerateWeekDays()
        {
            var persianCalendar = new PersianCalendar();
            var today = DateTime.Now;

            string[] days = { "شنبه", "یک‌شنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه" };

            int dayOfWeek = ((int)persianCalendar.GetDayOfWeek(today) + 1) % 7;

            for (int i = 0; i < 7 - dayOfWeek; i++)
            {
                var date = today.AddDays(i);
                var year = persianCalendar.GetYear(date);
                var month = persianCalendar.GetMonth(date);
                var day = persianCalendar.GetDayOfMonth(date);

                string persianDate = $"{day} {GetPersianMonthName(month)} {year}";

                CurrentWeekDays.Add(new DayInfoModel
                {
                    PersianDay = days[(dayOfWeek + i) % 7],
                    PersianDate = persianDate
                });
            }
        }

        private string GetPersianMonthName(int month)
        {
            string[] months = {
                "فروردین","اردیبهشت","خرداد","تیر","مرداد","شهریور",
                "مهر","آبان","آذر","دی","بهمن","اسفند"
            };
            return months[month - 1];
        }
    }
}
