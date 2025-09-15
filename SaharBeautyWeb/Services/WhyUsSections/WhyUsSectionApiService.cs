using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;
using SaharBeautyWeb.Services.Contracts;
using System.Net.Http.Headers;

namespace SaharBeautyWeb.Services.WhyUsSections;

public class WhyUsSectionApiService : IWhyUsSectionService
{
    private readonly HttpClient _client;
    private readonly ICRUDApiService _apiService;

    public WhyUsSectionApiService(
        HttpClient client,
        ICRUDApiService apiService,
        string? baseAddress = null)
    {
        _client = client;
        _apiService = apiService;
        _client.BaseAddress = new Uri(baseAddress!);
    }

    public async Task<ApiResultDto<long>> AddWhyUsSection(AddWhyUsSectionDto dto)
    {
        var url = "why-us-sections/add";
        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.Title), "Title");
        content.Add(new StringContent(dto.Title), "Description");
        var fileStream = dto.Image.OpenReadStream();
        var fileContent=new StreamContent(fileStream);
        fileContent.Headers.ContentType=new MediaTypeHeaderValue("application/json");
        content.Add(fileContent, "Image", dto.Image.FileName);


        var result = await _apiService.AddAsync<long>(url, content);
        return result;
    }

    public async Task<ApiResultDto<GetWhyUsSectionDto>> GetWhyUsSection()
    {
        var url = "why-us-sections";
        var result = await _apiService.GetAsync<GetWhyUsSectionDto>(url);

        return new ApiResultDto<GetWhyUsSectionDto>()
        {
            Data=result.Data,
            IsSuccess = result.IsSuccess,
            Error = result.Error
        };
    }
}
