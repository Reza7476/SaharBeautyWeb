using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Models;
using SaharBeautyWeb.Services.Contracts;

namespace SaharBeautyWeb.Services.Landing.WhyUsSections;

public class WhyUsSectionApiService : IWhyUsSectionService
{
    private readonly HttpClient _client;
    private readonly ICRUDApiService _apiService;
    private const string _controllerUrl = "why-us-sections";
    public WhyUsSectionApiService(
        HttpClient client,
        ICRUDApiService apiService,
        string? baseAddress = null)
    {
        _client = client;
        _apiService = apiService;
    }

    public async Task<ApiResultDto<GetWhyUsSectionDto>> GetWhyUsSection()
    {
        var url = $"{_controllerUrl}";
        var result = await _apiService.GetAsync<GetWhyUsSectionDto>(url);

        return new ApiResultDto<GetWhyUsSectionDto>()
        {
            Data = result.Data,
            IsSuccess = result.IsSuccess,
            Error = result.Error,
            StatusCode = result.StatusCode,
        };
    }

    public async Task<ApiResultDto<WhyUsSectionModel_Edit?>> GetWhyUsSectionById(long id)
    {
        var url = $"{_controllerUrl}/{id}";
        var result = await _apiService.GetAsync<WhyUsSectionModel_Edit>(url);
        return new ApiResultDto<WhyUsSectionModel_Edit?>()
        {
            Data = result.Data,
            Error = result.Error,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode
        };
    }

    public async Task<ApiResultDto<GetWhyUsSectionForLandingDto>>
        GetWhyUsSectionForLanding()
    {
        var url = $"{_controllerUrl}/all-for-landing";
        var result = await _apiService.GetAsync<GetWhyUsSectionForLandingDto>(url);
        return new ApiResultDto<GetWhyUsSectionForLandingDto>()
        {
            Data = result.Data,
            Error = result.Error,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode
        };

    }
}
