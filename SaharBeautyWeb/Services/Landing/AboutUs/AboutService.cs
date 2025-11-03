using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Dtos;
using SaharBeautyWeb.Services.Contracts;

namespace SaharBeautyWeb.Services.Landing.AboutUs;

public class AboutService : IAboutUsService
{
    private readonly HttpClient _client;
    private readonly ICRUDApiService _apiService;
    private const string _controllerUrl = "contact-us";

    public AboutService(HttpClient client,
        ICRUDApiService apiService,
        string? baseAddress = null)
    {
        _client = client;
        _apiService = apiService;
    }

    public async Task<ApiResultDto<GetAboutUsDto?>> GeAboutUs()
    {

        var url = $"{_controllerUrl}";

        var result = await _apiService.GetAsync<GetAboutUsDto?>(url);
        return new ApiResultDto<GetAboutUsDto?>()
        {
            Data = result.Data,
            Error = result.Error,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode

        };
    }

    public async Task<ApiResultDto<GetAboutUsDto?>> GeAboutUsById(long id)
    {
        var url = $"{_controllerUrl}/{id}";

        var result = await _apiService.GetAsync<GetAboutUsDto?>(url);
        return result;
    }
}
