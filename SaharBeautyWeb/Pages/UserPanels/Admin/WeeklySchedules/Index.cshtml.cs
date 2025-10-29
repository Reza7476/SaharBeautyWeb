using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Dtos;
using SaharBeautyWeb.Models.Entities.WeeklySchedules.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Admin.WeeklySchedules;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.WeeklySchedules;

public class IndexModel : AjaxBasePageModel
{
    private readonly IWeeklyScheduleService _services;

    public IndexModel(IWeeklyScheduleService services,
        ErrorMessages errorMessage) : base(errorMessage)
    {
        _services = services;
    }

    public List<GetScheduleModel> GetSchedules { get; set; } = new();
    [BindProperty]
    public SaveScheduleModel NewSchedule { get; set; } = default!;


    public async Task<IActionResult> OnGet()
    {
        var result = await _services.GetSchedules();

        var apiResult = new ApiResultDto<List<GetScheduleModel>>()
        {
            Error = result.Error,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode,
            Data = result.Data!.Select(sh => new GetScheduleModel()
            {
                DayOfWeek =  sh.DayOfWeek,
                EndTime = sh.EndTime,
                Id = sh.Id,
                IsActive = sh.IsActive,
                StartTime = sh.StartTime
            }).ToList()

        };
        var response = HandleApiResult(apiResult);
        if (response is PageResult)
            GetSchedules = apiResult.Data!;
        return response;
    }

    public async Task<IActionResult> OnPostSaveNewSchedule()
    {

        var start = DateTime.Today.Add(TimeSpan.Parse(NewSchedule.StartTime));
        var end = DateTime.Today.Add(TimeSpan.Parse(NewSchedule.EndTime));
        if (NewSchedule.Id == 0)
        {
            var newSchedule = await _services.Add(new AddNewScheduleDto()
            {
                DayOfWeek = (DayWeek)NewSchedule.DayOfWeek,
                EndTime = end,
                StartTime = start,
                IsActive = NewSchedule.IsActive
            });

            return HandleApiAjxResult(newSchedule);
        }
        else
        {
            var editSchedule = await _services.Edit(new EditScheduleDto()
            {
                DayOfWeek = (DayWeek)NewSchedule.DayOfWeek,
                EndTime = end,
                StartTime = start,
                IsActive = NewSchedule.IsActive,
                Id = NewSchedule.Id
            });

           return HandleApiAjxResult(editSchedule);
        }
    }
}

