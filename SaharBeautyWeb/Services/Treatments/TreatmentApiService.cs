using SaharBeautyWeb.Models.Commons;
using SaharBeautyWeb.Pages.UserPanels.Admin.SiteSettings.Treatments.Dtos;
using SaharBeautyWeb.Services.Contracts;

namespace SaharBeautyWeb.Services.Treatments;

public class TreatmentApiService : ITreatmentService
{
    private readonly HttpClient _client;
    private readonly ICrudApiService _apiService;

    public TreatmentApiService(HttpClient client,
        ICrudApiService apiService,
        string? baseAddress = null)
    {
        _client = client;
        _apiService = apiService;
        _client.BaseAddress = new Uri(baseAddress!);
    }


    public async Task<ApiResultDto<List<GetAllTreatmentDto>>> GetAll()
    {
        var result = await _apiService.GetAllAsync<GetAllTreatmentDto>("treatments/all");

        if (!result.IsSuccess|| result.Error != null)
            return new ApiResultDto<List<GetAllTreatmentDto>>
            {
                Error = result.Error,
                IsSuccess = result.IsSuccess,
            };
        var mapped = result.Data?.Select(r => new GetAllTreatmentDto()
        {
            Description = r.Description,
            Media = new MediaDto
            {
                Extension = r.Media?.Extension ?? " ",
                ImageName = r.Media?.ImageName ?? " ",
                UniqueName = r.Media? .UniqueName ?? " ",
                Url = r.Media? .Url?? " " 
            },
            Id = r.Id,
            Title = r.Title,
        }).ToList();

        return new ApiResultDto<List<GetAllTreatmentDto>>
        {
            Data = mapped,
            IsSuccess = true,
            StatusCode = result.StatusCode
        };
    }
}
