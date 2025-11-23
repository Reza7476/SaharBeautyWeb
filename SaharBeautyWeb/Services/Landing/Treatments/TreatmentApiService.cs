using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Models.Entities.Treatments.Models.Landing;
using SaharBeautyWeb.Services.Contracts;

namespace SaharBeautyWeb.Services.Landing.Treatments;

public class TreatmentApiService : ITreatmentService
{
    private readonly HttpClient _client;
    private readonly ICRUDApiService _apiService;
    private const string _controllerUrl = "treatments";


    public TreatmentApiService(HttpClient client,
        ICRUDApiService apiService,
        string? baseAddress = null)
    {
        _client = client;
        _apiService = apiService;
    }

    public async Task<ApiResultDto<GetAllDto<GetTreatmentDto>>> GetAll(int? offset = null, int? limit = null)
    {
        var url = $"{_controllerUrl}/all";
        var result = await _apiService.GetAllAsync<GetTreatmentDto>(url, offset, limit);

        if (!result.IsSuccess || result.Error != null)
            return new ApiResultDto<GetAllDto<GetTreatmentDto>>
            {
                Error = result.Error,
                IsSuccess = result.IsSuccess,
            };
        var mapped = new GetAllDto<GetTreatmentDto>()
        {
            Elements = result.Data.Elements,
            TotalElements = result.Data.TotalElements
        };

        return new ApiResultDto<GetAllDto<GetTreatmentDto>>
        {
            Data = mapped,
            IsSuccess = true,
            StatusCode = result.StatusCode
        };
    }

    public async Task<ApiResultDto<GetTreatmentWithAllImagesDto?>> GetById(long id)
    {
        var url = $"treatments/{id}";
        var result = await _apiService.GetAsync<GetTreatmentWithAllImagesDto>(url);
        return new ApiResultDto<GetTreatmentWithAllImagesDto?>()
        {
            Data = result.Data,
            Error = result.Error,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode
        };
    }

    public async Task<ApiResultDto<List<GetTreatmentsForLandingDto>>> GetForLanding()
    {

        var url = $"{_controllerUrl}/for-landing";

        var result = await _apiService.GetAsync<List<GetTreatmentsForLandingDto>>(url);
        return result;
    }

   
}
