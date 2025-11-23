
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Treatments.Dtos;
using System.Text.Json;
using System.Text;
using SaharBeautyWeb.Models.Entities.Treatments.Models;
using SaharBeautyWeb.Models.Entities.Review_Comment.Model;

namespace SaharBeautyWeb.Services.UserPanels.Admin.Treatments;

public class TreatmentUserPanelService : UserPanelBaseService, ITreatmentUserPanelService
{
    private const string _apiUrl = "treatments";

    public TreatmentUserPanelService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<long>> Add(AddTreatmentDto dto)
    {
        var url = $"{_apiUrl}/add";

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.Title ?? " "), "Title");
        content.Add(new StringContent(dto.Description ?? " "), "Description");
        content.Add(new StringContent(dto.Duration.ToString()), "Duration");
        content.Add(new StringContent(dto.Price.ToString()), "Price");
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

    public async Task<ApiResultDto<List<GetAllTreatmentForAppointmentModel>>> GetAllForAppointment()
    {
        var url = $"{_apiUrl}/all-for-appointment";
        var result = await GetAsync<List<GetAllTreatmentForAppointmentModel>>(url);
        return result;
    }

    public async Task<ApiResultDto<GetTreatmentForAppointmentDto?>> GetDetails(long id)
    {
        var url = $"{_apiUrl}/{id}/for-appointment";

        var result = await GetAsync<GetTreatmentForAppointmentDto?>(url);
        return result;
    }

    public async Task<ApiResultDto<GetAllDto<GetAllTreatmentImagesModel>>> GetGalleryImage(int offset, int limit)
    {
        var url = $"{_apiUrl}/all-image-gallery-for-landing";
        var query = new List<string>()
        {
            $"Offset={offset}",
            $"Limit={limit}",
        };
        if (query.Any())
        {
            url = url + "?" + string.Join("&", query);
        }


        var result = await GetAsync<GetAllDto<GetAllTreatmentImagesModel>>(url);
        if (!result.IsSuccess || result.Error != null)
        {
            return new ApiResultDto<GetAllDto<GetAllTreatmentImagesModel>>
            {
                Error = result.Error,
                IsSuccess = result.IsSuccess
            };
        }
        var mapped = new GetAllDto<GetAllTreatmentImagesModel>()
        {
            Elements = result.Data.Elements,
            TotalElements = result.Data.TotalElements,
        };

        return new ApiResultDto<GetAllDto<GetAllTreatmentImagesModel>>
        {
            Data = mapped,
            IsSuccess = true,
            StatusCode = result.StatusCode
        };
        throw new NotImplementedException();
    }

    public async Task<ApiResultDto<List<GetPopularTreatmentsDto>>> GetPopularTreatments()
    {
        var url = $"{_apiUrl}/popular";

        var result = await GetAsync<List<GetPopularTreatmentsDto>>(url);
        return result;
    }

    public async Task<ApiResultDto<List<GetTreatmentTitleDto>>> GetTitlesForAdmin()
    {
        var url = $"{_apiUrl}/all-for-admin-appointment-list";
        var result = await GetAsync<List<GetTreatmentTitleDto>>(url);
        return result;
    }

    public async Task<ApiResultDto<object>>
        UpdateTitleAndDescription(UpdateTreatmentTitleAndDescriptionDto dto)
    {
        var url = $"{_apiUrl}/{dto.Id}";
        var json = JsonSerializer.Serialize(new
        {
            dto.Title,
            dto.Description,
            dto.Duration,
            dto.Price
        });

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await PutAsync<object>(url, content);
        return result;
    }
}
