using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.Banners.Management.Dtos;
using System.Net.Http.Headers;

namespace SaharBeautyWeb.Services.UserPanels.Admin.Banners;

public class BannerUserPanelService : UserPanelBaseService, IBannerUserPanelService
{

    private const string _apiUrl = "banners";
    public BannerUserPanelService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<long>> Add(AddBannerDto dto)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.Title ?? ""), "Title");
        var url = $"{_apiUrl}/add";
        if (dto.Image != null)
        {
            var fileStream = dto.Image.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.Image.ContentType);
            content.Add(fileContent, "Image", dto.Image.FileName);
        }

        return await PostAsync<long>(url, content);
    }

    public async Task<ApiResultDto<long>> UpdateBanner(UpdateBannerDto dto)
    {
        var url = $"{_apiUrl}/{dto.Id}";
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.Title ?? ""), "Title");

        if (dto.Image != null)
        {
            var fileStream = dto.Image.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.Image.ContentType);
            content.Add(fileContent, "Image", dto.Image.FileName);
        }
        var result = await PatchAsync<long>(url, content);
        return result;
    }
}
