using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Services.Contracts;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.Treatments;

public class TreatmentApiService : ITreatmentService
{
    private readonly HttpClient _client;
    private readonly ICRUDApiService _apiService;

    public TreatmentApiService(HttpClient client,
        ICRUDApiService apiService,
        string? baseAddress = null)
    {
        _client = client;
        _apiService = apiService;
        _client.BaseAddress = new Uri(baseAddress!);
    }

    public async Task<ApiResultDto<long>> Add(AddTreatmentModel dto)
    {

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.Title ?? " "), "Title");
        content.Add(new StringContent(dto.Description ?? " "), "Description");
        if (dto.Image != null)
        {
            var fileStream = dto.Image.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            content.Add(fileContent, "Media", dto.Image.FileName);
        }

        var result = await _apiService.AddFromFormAsync<long>("treatments/add", content);
        return result;
    }

    public async Task<ApiResultDto<long>> AddImage(AddMediaDto dto)
    {

        string url = $"treatments/{dto.Id}/add-image";
        using var content = new MultipartFormDataContent();
        if (dto.AddMedia != null)
        {
            var fileSteam = dto.AddMedia.OpenReadStream();
            var fileContent = new StreamContent(fileSteam);
            content.Add(fileContent, "Media", dto.AddMedia.FileName);
        }

        var result = await _apiService.AddFromFormAsync<long>(url, content);

        return result;
    }

    public async Task<ApiResultDto<object>> DeleteImage(long imageId, long id)
    {
        string url = $"treatments/{id}/{imageId}/image";
        var result = await _apiService.Delete<object>(url);
        return new ApiResultDto<object>
        {
            Data = result.Data,
            Error = result.Error,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode
        };
    }

    public async Task<ApiResultDto<GetAllDto<GetTreatmentDto>>> GetAll(int? offset = null, int? limit = null)
    {
        var result = await _apiService.GetAllAsync<GetTreatmentDto>("treatments/all", offset, limit);

        if (!result.IsSuccess || result.Error != null)
            return new ApiResultDto<GetAllDto<GetTreatmentDto>>
            {
                Error = result.Error,
                IsSuccess = result.IsSuccess,
            };
        var mapped =new GetAllDto<GetTreatmentDto>()
        {
            Elements=result.Data.Elements,
            TotalElements=result.Data.TotalElements
        };

        return new ApiResultDto<GetAllDto<GetTreatmentDto>>
        {
            Data = mapped,
            IsSuccess = true,
            StatusCode = result.StatusCode
        };
    }

    public async Task<ApiResultDto<GetTreatmentWithAllImagesDto>> GetById(long id)
    {
        var url = $"treatments/{id}";
        var result = await _apiService.GetAsync<GetTreatmentWithAllImagesDto>(url);
        var a = new ApiResultDto<GetTreatmentWithAllImagesDto>()
        {
            Data = result.Data,
            Error = result.Error,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode
        };
        return a;
    }

    public async Task<ApiResultDto<object>>
        UpdateTitleAndDescription(UpdateTreatmentTitleAndDescriptionDto dto)
    {
        var url = $"treatments/{dto.Id}";
        var json = JsonSerializer.Serialize(new
        {
            Title = dto.Title,
            Description = dto.Description
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _apiService.UpdateAsPutAsyncFromBody<object>(url, content);
        return result;
    }
}
