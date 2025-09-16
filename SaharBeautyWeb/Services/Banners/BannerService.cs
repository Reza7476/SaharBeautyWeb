using Microsoft.AspNetCore.Components.Forms;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Banners;
using SaharBeautyWeb.Services.Contracts;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SaharBeautyWeb.Services.Banners;

public class BannerService : IBannerService
{
    private readonly HttpClient _client;
    private readonly ICRUDApiService _apiService;


    public BannerService(
        HttpClient client,
        ICRUDApiService apiService,
        string? baseAddress = null)
    {
        _client = client;
        _client.BaseAddress = new Uri(baseAddress!);
        _apiService = apiService;
    }

    public async Task<ApiResultDto<long>> Add(AddBannerModel dto)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(dto.Title ?? ""), "Title");

        if (dto.Image != null)
        {
            var fileStream = dto.Image.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.Image.ContentType);
            content.Add(fileContent, "Image", dto.Image.FileName);
        }

        var result = await _apiService.AddFromFormAsync<long>("banners/add", content);

        return result;
    }



    public async Task<ApiResultDto<GetBannerDto?>> Get()
    {
       
        var result = await _apiService.GetAsync<GetBannerDto?>("banners");
        return result;
    }

    public  async Task<ApiResultDto<long>> UpdateBanner(UpdateBannerDto dto)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(dto.Title ?? ""), "Title");

        if (dto.Image != null)
        {
            var fileStream = dto.Image.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.Image.ContentType);
            content.Add(fileContent, "Image", dto.Image.FileName);
        }

        var result = await _apiService.UpdateAsPatchAsync<long>($"banners/{dto.Id}", content);
        return result;
    }
}



