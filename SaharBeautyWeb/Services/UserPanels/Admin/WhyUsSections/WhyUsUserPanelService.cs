
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.UserPanels.WhyUsSections;

public class WhyUsUserPanelService : UserPanelBaseService, IWhyUsUserPanelService
{

    private const string _apiUrl = "why-us-sections";
    public WhyUsUserPanelService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<long>> AddWhyUsQuestions(AddWhyUsQuestionsDto dto)
    {
        var url = $"{_apiUrl}/{dto.WhyUsSectionId}/add-question";
        var json = JsonSerializer.Serialize(new
        {
            dto.Answer,
            dto.Question
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await PostAsync<long>(url, content);
        return result;
    }

    public async Task<ApiResultDto<long>> AddWhyUsSection(AddWhyUsSectionDto dto)
    {
        var url = $"{_apiUrl}/add";

        var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.Title), "Title");
        content.Add(new StringContent(dto.Description), "Description");
        var fileStream = dto.Image.OpenReadStream();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        content.Add(fileContent, "Image", dto.Image.FileName);

        var result = await PostAsync<long>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> DeleteQuestion(long questionId)
    {
        var url = $"{_apiUrl}/{questionId}/question";
        var result = await DeleteAsync<object>(url);
        return result;
    }

    public async Task<ApiResultDto<object>> EditImage(AddMediaDto dto)
    {
        var url = $"{_apiUrl}/{dto.Id}/image";
        var content = new MultipartFormDataContent();
        var fileStream = dto.AddMedia.OpenReadStream();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        content.Add(fileContent, "Media", dto.AddMedia.FileName);

        var result=await PatchAsync<object>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> EditTitleAndDescription(EditWhyUsSectionTitleAndDescriptionDto dto)
    {
        var url = $"{_apiUrl}/{dto.Id}";
        var json = JsonSerializer.Serialize(new
        {
            dto.Title,
            dto.Description
        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PutAsync<object>(url, content);
        return result;
    }
}
