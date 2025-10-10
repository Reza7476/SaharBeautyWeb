using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.Treatments;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Treatments
{
    public class IndexModel : AjaxBasePageModel
    {
        private readonly ITreatmentService _service;
        public GetAllTreatmentModel ListModel { get; set; } = new();

        [BindProperty]
        public AddTreatmentModel AddModel { get; set; }

        [BindProperty]
        public TreatmentDetailsDto ModelData { get; set; }

        public IndexModel(ITreatmentService service,
            ErrorMessages errorMessage) : base(errorMessage)
        {
            _service = service;
        }


        public async Task<IActionResult> OnGet(int pageNumber = 0, int limit = 5)
        {
            int offset = pageNumber;

            var result = await _service.GetAll(offset, limit);
            var response = HandleApiResult(result);
            if (result.IsSuccess && result.Data != null)
            {
                ListModel.Treatments = result.Data.Elements;
                ListModel.TotalElements = result.Data.TotalElements;
                ListModel.CurrentPage = pageNumber;
                ListModel.TotalPages = result.Data.TotalElements.ToTotalPage(limit); ;
            }
            return response;
        }

        public PartialViewResult OnGetAddTreatmentPartial()
        {
            return Partial("_AddPartial");
        }

        public async Task<IActionResult> OnPostAddTreatment()
        {
            var (isValid, message) = AddModel.Image.ValidateImage();

            if (!isValid)
            {
                return new JsonResult(new
                {
                    success = false,
                    error = message
                });
            }

            if (
                string.IsNullOrWhiteSpace(AddModel.Title) ||
                string.IsNullOrWhiteSpace(AddModel.Description))
            {
                return new JsonResult(new
                {
                    success = false,
                    error = "عنوان یا توضیحات  ناقص میباشند"
                });
            }

            var result = await _service.Add(new AddTreatmentModel
            {
                Description = AddModel.Description,
                Image = AddModel.Image,
                Title = AddModel.Title
            });
            return HandleApiAjxResult(result);
        }

        public async Task<IActionResult> OnGetEditTreatmentPartial(long id)
        {
            var result = await _service.GetById(id);
            var response = HandleApiAjaxPartialResult(
                result, data => new TreatmentDetailsDto()
                {
                    Description = data.Description,
                    Title = data.Title,
                    Media = result.Data!.Media,
                    Id = id
                }, "_editTreatmentPartial");
            return response;
        }

        public async Task<IActionResult> OnPostAddImage()
        {
            var (isValid, message) = ModelData.AddMedia.ValidateImage();
            if (!isValid)
                return new JsonResult(new
                {
                    success = isValid,
                    error = message
                });
            var image = await _service.AddImage(new AddMediaDto
            {
                AddMedia = ModelData.AddMedia,
                Id = ModelData.Id
            });

            return new JsonResult(new
            {
                data = image.Data,
                success = image.IsSuccess,
                statusCode = image.StatusCode,
                error = image.Error
            });
        }

        public async Task<IActionResult> OnPostDeleteImage(long imageId, long id)
        {
            var result = await _service.DeleteImage(imageId, id);
            return new JsonResult(new
            {
                data = result,
                success = result.IsSuccess,
                error = result.Error,

            });
        }

        public async Task<IActionResult> OnPostEditTreatment()
        {
            if (ModelData.Title == null || ModelData.Description == null)
            {
                return new JsonResult(new
                {
                    error = "عنوان یا توضیحات نمیتواند خالی باشند",
                    success = false
                });
            }

            var result = await _service.UpdateTitleAndDescription(new UpdateTreatmentTitleAndDescriptionDto()
            {
                Description = ModelData.Description,
                Title = ModelData.Title,
                Id = ModelData.Id
            });


            return new JsonResult(new
            {
                data = result,
                success = result.IsSuccess,
                error = result.Error,
            });
        }
    }
}
