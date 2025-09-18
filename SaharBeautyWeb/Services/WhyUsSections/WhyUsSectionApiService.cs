using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Models;
using SaharBeautyWeb.Services.Contracts;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.WhyUsSections;

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
        _client.BaseAddress = new Uri(baseAddress!);
    }

    public async Task<ApiResultDto<long>> AddWhyUsQuestions(AddWhyUsQuestionsDto dto)
    {
        var url = $"{_controllerUrl}/{dto.WhyUsSectionId}/add-question";
        var json = JsonSerializer.Serialize(new
        {
            dto.Answer,
            dto.Question
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _apiService.AddFromBodyAsync<long>(url, content);
        return result;
    }

    public async Task<ApiResultDto<long>> AddWhyUsSection(AddWhyUsSectionDto dto)
    {
        var url = $"{_controllerUrl}/add";
        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.Title), "Title");
        content.Add(new StringContent(dto.Title), "Description");
        var fileStream = dto.Image.OpenReadStream();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        content.Add(fileContent, "Image", dto.Image.FileName);


        var result = await _apiService.AddFromFormAsync<long>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> DeleteQuestion(long questionId)
    {
        var url = $"{_controllerUrl}/{questionId}/question";
        var result = await _apiService.Delete<object>(url);
        return result;
    }

    public async Task<ApiResultDto<object>> 
        EditTitleAndDescription(EditWhyUsSectionTitleAndDescriptionDto dto)
    {
        var url = $"{_controllerUrl}/{dto.Id}";
        var json = JsonSerializer.Serialize(new
        {
            dto.Title,
            dto.Description
        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _apiService.UpdateAsPutFromBodyAsync<object>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetWhyUsSectionDto>> GetWhyUsSection()
    {
        var url = $"{_controllerUrl}";
        var result = await _apiService.GetAsync<GetWhyUsSectionDto>(url);

        return new ApiResultDto<GetWhyUsSectionDto>()
        {
            Data = result.Data,
            IsSuccess = result.IsSuccess,
            Error = result.Error
        };
    }

    public async Task<ApiResultDto<WhyUsSectionModel_Edit>> GetWhyUsSectionById(long id)
    {
        var url = $"{_controllerUrl}/{id}";
        var result = await _apiService.GetAsync<WhyUsSectionModel_Edit>(url);
        return new ApiResultDto<WhyUsSectionModel_Edit>()
        {
            Data = result.Data,
            Error = result.Error,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode
        };
    }
}
