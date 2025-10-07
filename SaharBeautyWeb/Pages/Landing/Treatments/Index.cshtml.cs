using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.Treatments;

namespace SaharBeautyWeb.Pages.Landing.Treatments
{
    public class IndexModel : LandingBasePageModel
    {
        private readonly ITreatmentService _service;

        public GetAllTreatmentForLandingModel ListModel { get; set; } = new();

        public IndexModel(ITreatmentService service,
            ErrorMessages errorMessage) : base(errorMessage)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int pageNumber = 0, int limit = 3)
        {
            int offset = pageNumber;
            var result = await _service.GetAll(offset, limit);

            var apiResult = new ApiResultDto<GetAllTreatmentForLandingModel>()
            {
                Data = result.Data != null
                    ? new GetAllTreatmentForLandingModel()
                    {
                        CurrentPage = pageNumber,
                        TotalElements = result.Data.TotalElements,
                        TotalPages = result.Data.TotalElements.ToTotalPage(limit),
                        Treatments = result.Data.Elements,
                    }
                    : null,
                Error = result.Error,
                StatusCode = result.StatusCode,
                IsSuccess = result.IsSuccess,
            };
            var actionResult = HandleApiResult(apiResult);
            if(actionResult is PageResult)
            {
                ListModel = apiResult.Data ?? new GetAllTreatmentForLandingModel();
            }

            return actionResult;
        }
    }
}
