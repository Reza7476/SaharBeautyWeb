using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Dtos;
using SaharBeautyWeb.Services.Contracts;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.AboutUs;

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
        _client.BaseAddress = new Uri(baseAddress!);
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

    public async Task<ApiResultDto<long>> Add(AddAboutUsDto dto)
    {
        var url = $"{_controllerUrl}/add";

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.MobileNumber), "MobileNumber");
        content.Add(new StringContent(dto.Address??""), "Address");
        content.Add(new StringContent(dto.Email??""), "Email");
        if (dto.Latitude.HasValue)
        {
            content.Add(new StringContent(dto.Latitude.Value.ToString(CultureInfo.InvariantCulture)), "Latitude");
        }
        if (dto.Longitude.HasValue)
        {
            content.Add(new StringContent(dto.Longitude.Value.ToString(CultureInfo.InvariantCulture)), "Longitude");
        }
        content.Add(new StringContent(dto.Description ?? ""), "Description");
        content.Add(new StringContent(dto.Telephone ?? ""), "Telephone");
        content.Add(new StringContent(dto.Instagram ?? ""), "Instagram");
        if (dto.LogoImage != null)
        {
            var fileStream = dto.LogoImage.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            content.Add(fileContent, "LogoImage");
        }

        var result = await _apiService.AddFromFormAsync<long>(url, content);

        return result;
    }

    public async Task<ApiResultDto<GetAboutUsDto?>> GeAboutUsById(long id)
    {
        var url = $"{_controllerUrl}/{id}";

        var result = await _apiService.GetAsync<GetAboutUsDto?>(url);
        return result;
    }

    public async Task<ApiResultDto<object>> Edit(EditAboutUsDto dto)
    {
        var url = $"{_controllerUrl}/{dto.Id}";
        var json = JsonSerializer.Serialize(new
        {
            dto.MobileNumber,
            dto.Latitude,
            dto.Longitude,
            dto.Telephone,
            dto.Description,
            dto.Address,
            dto.Email,
            dto.Instagram
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result=await _apiService.UpdateAsPutFromBodyAsync<object>(url, content);
        return result;

    }
}
