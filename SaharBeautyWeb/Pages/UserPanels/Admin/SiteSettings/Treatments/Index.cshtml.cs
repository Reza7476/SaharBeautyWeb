using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Services.Treatments;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Treatments
{
    public class IndexModel : PageModel
    {
        private readonly ITreatmentService _service;
        public GetAllTreatmentModel ListModel { get; set; } = new();

        [BindProperty]
        public AddTreatmentModel AddModel { get; set; }

        [BindProperty]
        public TreatmentDetailsDto ModelData { get; set; }

        public IndexModel(ITreatmentService service)
        {
            _service = service;
        }


        public async Task OnGet(int pageNumber = 0, int limit = 5)
        {
            int offset = pageNumber;       
            
            var treatments = await _service.GetAll(offset, limit);

            if (treatments.IsSuccess && treatments.Data != null)
            {
                ListModel.Treatments = treatments.Data.Elements;
                ListModel.TotalElements = treatments.Data.TotalElements;
                ListModel.CurrentPage = pageNumber;
                ListModel.TotalPages= treatments.Data.TotalElements.ToTotalPage(limit); ;
            }
            else
            {
                ViewData["ErrorMessage"] = treatments.Error ?? "خطایی پیش آمده";
            }
        }

        public PartialViewResult OnGetAddTreatmentPartial()
        {
            return Partial("_AddPartial");
        }

        public async Task<IActionResult> OnPostAddTreatment()
        {
            if (AddModel.Image == null ||
                string.IsNullOrWhiteSpace(AddModel.Title) ||
                string.IsNullOrWhiteSpace(AddModel.Description))
            {
                return new JsonResult(new
                {
                    success = false,
                    error = "فیلد های ناقص میباشند"
                });
            }

            var treatment = await _service.Add(new AddTreatmentModel
            {
                Description = AddModel.Description,
                Image = AddModel.Image,
                Title = AddModel.Title
            });

            return new JsonResult(new
            {
                data = treatment.Data,
                success = treatment.IsSuccess,
                statusCode = treatment.StatusCode,
                error = treatment.Error
            });
        }

        public async Task<PartialViewResult> OnGetEditTreatmentPartial(long id)
        {
            var treatment = await _service.GetById(id);

            var model = new TreatmentDetailsDto()
            {
                Description = treatment?.Data?.Description,
                Title = treatment?.Data?.Title,
                Media = treatment.Data.Media,
                Id = id

            };
            return Partial("_editTreatmentPartial", model);
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
