
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Dtos;
using System.Text.Json;
using System.Text;

namespace SaharBeautyWeb.Services.UserPanels.Treatments;

public class TreatmentUserPanelService : UserPanelBaseService, ITreatmentUserPanelService
{
    private const string _apiUrl = "treatments";

    public TreatmentUserPanelService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<long>> Add(AddTreatmentModel dto)
    {
        var url = $"{_apiUrl}/add";

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.Title ?? " "), "Title");
        content.Add(new StringContent(dto.Description ?? " "), "Description");
        if (dto.Image != null)
        {
            var fileStream = dto.Image.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            content.Add(fileContent, "Media", dto.Image.FileName);
        }

        var result = await PostAsync<long>(url, content);
        return result;
    }

    public async Task<ApiResultDto<long>> AddImage(AddMediaDto dto)
    {
        var url = $"{_apiUrl}/{dto.Id}/add-image";
        using var content = new MultipartFormDataContent();
        if (dto.AddMedia != null)
        {
            var fileSteam = dto.AddMedia.OpenReadStream();
            var fileContent = new StreamContent(fileSteam);
            content.Add(fileContent, "Media", dto.AddMedia.FileName);
        }

        var result = await PostAsync<long>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> DeleteImage(long imageId, long id)
    {
        var url = $"{_apiUrl}/{id}/{imageId}/image";
        var result = await DeleteAsync<object>(url);
        return new ApiResultDto<object>
        {
            Data = result.Data,
            Error = result.Error,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode
        };

    }


    public async Task<ApiResultDto<object>>
        UpdateTitleAndDescription(UpdateTreatmentTitleAndDescriptionDto dto)
    {
        var url = $"{_apiUrl}/{dto.Id}";
        var json = JsonSerializer.Serialize(new
        {
            Title = dto.Title,
            Description = dto.Description
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PutAsync<object>(url, content);
        return result;
    }
}
