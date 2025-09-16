using Autofac.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Models;
using SaharBeautyWeb.Services.WhyUsSections;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.WhyUsSections;

public class IndexModel : PageModel
{
    [BindProperty]
    public WhyUsSectionModel ModelData { get; set; } = new();
    
    [BindProperty]
    public AddWhyUsQuestionModel WhyUsQuestionModel { get; set; }    

    private readonly IWhyUsSectionService _service;

    public IndexModel(IWhyUsSectionService service)
    {
        _service = service;
    }

    public async Task OnGet()
    {
        var whyUsSection = await _service.GetWhyUsSection();

        if (whyUsSection.IsSuccess && whyUsSection.Data != null)
        {
            ModelData.Title = whyUsSection.Data.Title;
            ModelData.Description = whyUsSection.Data.Description;
            ModelData.Id = whyUsSection.Data.Id;
            ModelData.QuestionModels = whyUsSection.Data.Questions.Select(_ => new WhyUsQuestionModel
            {
                Answer = _.Answer,
                Question = _.Question,
                Id = _.Id
            }).ToList();
            ModelData.Media = whyUsSection.Data.Image;
        }
        else
        {
            ViewData["ErrorMessage"] = whyUsSection.Error ?? " خطایی پیش آمده";
        }
    }

    public async Task<IActionResult> OnPostCreateWhyUsSection()
    {
        if (ModelData.Title == null || ModelData.Description == null)
        {
            return new JsonResult(new
            {
                success = false,
                error = "فیلد های را پر کنید "
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
        var result = await _service.AddWhyUsSection(new AddWhyUsSectionDto()
        {
            Description = ModelData.Description,
            Image = ModelData.Image!,
            Title = ModelData.Title
        });
        return new JsonResult(new
        {
            data=result.Data,
            success=result.IsSuccess,
            statusCode=result.StatusCode,
            error=result.Error
        });

    }

    public async Task<PartialViewResult> OnGetAddWhyUsQuestionPartial(long id)
    {
        var model = new AddWhyUsQuestionModel
        {
            WhyUsSectionId = id
        };

        return Partial("_AddWhyUsQuestion", model);
    }

    public async Task<IActionResult> OnPostAddQuestions()
    {
       if(string.IsNullOrWhiteSpace(WhyUsQuestionModel.Question)||
            string.IsNullOrWhiteSpace(WhyUsQuestionModel.Answer)||
            WhyUsQuestionModel.WhyUsSectionId <= 0)
        {
            return new JsonResult(new
            {
                success = false,
                error="داده نا معتبر "
            });
        }
        var question = await _service.AddWhyUsQuestions(new AddWhyUsQuestionsDto()
        {
            Answer = WhyUsQuestionModel.Answer,
            Question = WhyUsQuestionModel.Question,
            WhyUsSectionId = WhyUsQuestionModel.WhyUsSectionId

        });

        return new JsonResult(new
        {
            success = question.IsSuccess,
            data=question.Data,
            Error=question.Error,
            StatusCode=question.StatusCode
        });
    }
}
