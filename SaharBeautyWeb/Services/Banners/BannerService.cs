using SaharBeautyWeb.Models.Commons;
using SaharBeautyWeb.Models.Entities.Banners;
using System.Text.Json;

namespace SaharBeautyWeb.Services.Banners;

public class BannerService : IBannerService
{
    private readonly HttpClient _client;

    public BannerService(HttpClient client, string? baseAddress = null)
    {
        _client = client;
        _client.BaseAddress = new Uri(baseAddress!);
    }

    public async Task<ApiResultDto<GetBannerDto?>> Get()
    {
        var response = await _client.GetAsync("banners");
        var resultContent = await response.Content.ReadAsStringAsync();
        var result = new ApiResultDto<GetBannerDto?>();
        result.StatusCode = (int)response.StatusCode;
        result.IsSuccess = response.IsSuccessStatusCode;

        if (response.IsSuccessStatusCode)
        {
            if (!string.IsNullOrEmpty(resultContent) && resultContent != null)
            {

                var banner = JsonSerializer.Deserialize<GetBannerDto?>(resultContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                result.Data = banner;
            }
            else
            {
                result.Data = null;
            }

        }
        else
        {
            result.Error = resultContent;
        }
        return result;
    }
}
