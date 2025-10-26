using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Dtos;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SaharBeautyWeb.Services.UserPanels.AboutUs;

public class AboutUsUserPanelService : UserPanelBaseService, IAboutUsUserPanelService
{
    private const string _apiUrl = "contact-us";

    public AboutUsUserPanelService(HttpClient client) : base(client)
    {
    }

    public Task<ApiResultDto<long>> Add(AddAboutUsDto dto)
    {
        var url = $"{_apiUrl}/add";

        var content = new MultipartFormDataContent();
        content.Add(new StringContent(dto.MobileNumber), "MobileNumber");
        content.Add(new StringContent(dto.Address ?? ""), "Address");
        content.Add(new StringContent(dto.Email ?? ""), "Email");
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
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.LogoImage.ContentType);
            content.Add(fileContent, "LogoImage", dto.LogoImage.FileName);
        }
        var result = PostAsync<long>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> Edit(EditAboutUsDto dto)
    {
        var url = $"{_apiUrl}/{dto.Id}";
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
        var result = await PutAsync<object>(url, content);
        return result;
    }

    public async Task<ApiResultDto<object>> EditLogo(EditMediaDto dto)
    {
        var url = $"{_apiUrl}/{dto.Id}/logo";

        var content = new MultipartFormDataContent();
        var fileStream = dto.Media!.OpenReadStream();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        content.Add(fileContent, "Media", dto.Media.FileName);

        var result = await PatchAsync<object>(url, content);
        return result;
    }
}
