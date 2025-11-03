using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.Landing.WhyUsSections;
using SaharBeautyWeb.Services.UserPanels.Admin.WhyUsSections;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.WhyUsSections;

public class IndexModel : AjaxBasePageModel
{
    [BindProperty]
    public WhyUsSectionModel ModelData { get; set; } = new();

    [BindProperty]
    public WhyUsSectionModel_Edit? EditModel { get; set; }

    [BindProperty]
    public AddWhyUsQuestionModel? WhyUsQuestionModel { get; set; }

    private readonly IWhyUsSectionService _service;
    private readonly IWhyUsUserPanelService _whyUsUserPanelService;

    public IndexModel(IWhyUsSectionService service,
        ErrorMessages errorMessage,
        IWhyUsUserPanelService whyUsUserPanelService) : base(errorMessage)
    {
        _service = service;
        _whyUsUserPanelService = whyUsUserPanelService;
    }

    public async Task<IActionResult> OnGet()
    {
        var result = await _service.GetWhyUsSection();
        var apiResult = new ApiResultDto<WhyUsSectionModel>()
        {
            Data = result.Data != null ? new WhyUsSectionModel()
            {

                Description = result.Data.Description,
                Id = result.Data.Id,
                Title = result.Data.Title,
                QuestionModels = result.Data.Questions.Select(q => new WhyUsQuestionModel()
                {
                    Id = q.Id,
                    Answer = q.Answer,
                    Question = q.Question

                }).ToList(),
                Media = result.Data.Image
            } : null,
            Error = result.Error,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode
        };

        var a = HandleApiResult(apiResult);

        if (a is PageResult)
            ModelData = apiResult.Data!;
        return a;

    }

    public async Task<IActionResult> OnPostCreateWhyUsSection()
    {
        if (ModelData.Title == null || ModelData.Description == null)
        {
            return new JsonResult(new
            {
                success = false,
                error = "فیلد های عنوان و توضیحات خالی هستند "
            });
        }
        var (isValid, message) = ModelData.Image.ValidateImage();
        if (!isValid)
        {
            return new JsonResult(new
            {
                success = isValid,
                error = message
            });
        }
        var result = await _whyUsUserPanelService.AddWhyUsSection(new AddWhyUsSectionDto()
        {
            Description = ModelData.Description,
            Image = ModelData.Image!,
            Title = ModelData.Title
        });

        return HandleApiAjxResult(result);
    }

    public async Task<PartialViewResult> OnGetAddWhyUsQuestionPartial(long id)
    {
        var model = new AddWhyUsQuestionModel
        {
            WhyUsSectionId = id
        };

        await Task.CompletedTask;
        return Partial("_AddWhyUsQuestion", model);

    }

    public async Task<IActionResult> OnPostAddQuestions()
    {
        if (string.IsNullOrWhiteSpace(WhyUsQuestionModel.Question) ||
             string.IsNullOrWhiteSpace(WhyUsQuestionModel.Answer) ||
             WhyUsQuestionModel.WhyUsSectionId <= 0)
        {
            return new JsonResult(new
            {
                success = false,
                error = "داده نا معتبر "
            });
        }
        var result = await _whyUsUserPanelService.AddWhyUsQuestions(new AddWhyUsQuestionsDto()
        {
            Answer = WhyUsQuestionModel.Answer,
            Question = WhyUsQuestionModel.Question,
            WhyUsSectionId = WhyUsQuestionModel.WhyUsSectionId

        });

        return HandleApiAjxResult(result);
    }

    public async Task<IActionResult> OnPostDeleteQuestion(long id)
    {
        var result = await _whyUsUserPanelService.DeleteQuestion(id);
        return HandleApiAjxResult(result);
    }

    public async Task<IActionResult> OnGetEditWhyUsSectionEdit(long id)
    {

        var result = await _service.GetWhyUsSectionById(id);
        return HandleApiAjaxPartialResult(result, data => new WhyUsSectionModel_Edit()
        {
            Description = data.Description,
            Title = data.Title,
            Image = data.Image,
            Id = id
        }, "_editWhyUsSectionPartial");
    }

    public async Task<IActionResult> OnPostApplyEditTitleAndDescription()
    {

        if (EditModel.Title == null || EditModel.Description == null)
        {
            return new JsonResult(new
            {
                Success = false,
                Error = "عنوان و توضیحات خالی هستند",
            });
        }

        var result = 
            await _whyUsUserPanelService.EditTitleAndDescription(
            new EditWhyUsSectionTitleAndDescriptionDto
            {
                Description = EditModel.Description,
                Title = EditModel.Title,
                Id = EditModel.Id
            });
        return HandleApiAjxResult(result);
    }

    public async Task<IActionResult> OnPostApplyEditWhyUsImage()
    {
        var (isValid, message) = EditModel.AddMedia.ValidateImage();

        if (EditModel?.Id == null || !isValid)
        {
            return new JsonResult(new
            {
                error = message ?? "داده نامعتبر ",
                succes = false
            });
        }
        var result = await _whyUsUserPanelService.EditImage(new AddMediaDto()
        {
            Id = EditModel.Id,
            AddMedia = EditModel.AddMedia!
        });
        return HandleApiAjxResult(result);
    }
}
