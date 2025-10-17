using SaharBeautyWeb.Models.Commons.Dtos;
using System.Text.Json;

public class UserPanelBaseService
{
    protected readonly HttpClient _client;

    public UserPanelBaseService(HttpClient client)
    {
        _client = client;
    }

    protected async Task<ApiResultDto<T>> SendRequestAsync<T>(
        HttpMethod method,
        string url,
        HttpContent? content = null)
    {
        var request = new HttpRequestMessage(method, url)
        {
            Content = content
        };

        var response = await _client.SendAsync(request);
        var raw = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (raw != "")
            {

                var data = JsonSerializer.Deserialize<T>(raw, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return new ApiResultDto<T>
                {
                    Data = data,
                    IsSuccess = true,
                    StatusCode = (int)response.StatusCode
                };
            }
            else
            {
                return new ApiResultDto<T>
                {
                    IsSuccess = true,
                    StatusCode = (int)response.StatusCode
                };
            }
        }

        return new ApiResultDto<T>
        {
            IsSuccess = false,
            Error = raw,
            StatusCode = (int)response.StatusCode
        };
    }


    protected Task<ApiResultDto<T>> GetAsync<T>(string url) => SendRequestAsync<T>(HttpMethod.Get, url);
    protected Task<ApiResultDto<T>> PostAsync<T>(string url, HttpContent content) => SendRequestAsync<T>(HttpMethod.Post, url, content);
    protected Task<ApiResultDto<T>> PutAsync<T>(string url, HttpContent content) => SendRequestAsync<T>(HttpMethod.Put, url, content);
    protected Task<ApiResultDto<T>> PatchAsync<T>(string url, HttpContent content) => SendRequestAsync<T>(HttpMethod.Patch, url, content);
    protected Task<ApiResultDto<T>> DeleteAsync<T>(string url) => SendRequestAsync<T>(HttpMethod.Delete, url);
}
