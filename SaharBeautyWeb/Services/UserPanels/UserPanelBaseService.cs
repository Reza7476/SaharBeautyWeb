using SaharBeautyWeb.Models.Commons.Dtos;
using System.Text.Json;

public class UserPanelBaseService
{
    protected readonly HttpClient _client;

    public UserPanelBaseService(HttpClient client)
    {
        _client = client;
    }

    protected async Task<ApiResultDto<T>> SendPostRequestAsync<T>(
        string url,
        HttpContent? content = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = content
        };

        var response = await _client.SendAsync(request);
        var raw = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (raw != "")
            {

                var data = JsonSerializer.Deserialize<T>(
                    raw,
                    new JsonSerializerOptions
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


    protected async Task<ApiResultDto<T>>
        SendPutRequestAsync<T>(string url, HttpContent content)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = content
        };
        return await PutAndPatchMethod<T>(request);
    }

    protected async Task<ApiResultDto<T>>
        SendPatchRequestAsync<T>(string url, HttpContent content)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, url)
        {
            Content = content
        };
        return await PutAndPatchMethod<T>(request);
    }



    protected Task<ApiResultDto<T>>
        SendGetRequestAsync<T>(HttpMethod get, string url)
    {
        throw new NotImplementedException();
    }



    private async Task<ApiResultDto<T>>
        SendDeleteRequestAsync<T>(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url)
        {
            Content = null
        };

        var response = await _client.SendAsync(request);
        var raw = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (raw != "")
            {

                var data = JsonSerializer.Deserialize<T>(
                    raw,
                    new JsonSerializerOptions
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

    private async Task<ApiResultDto<T>> PutAndPatchMethod<T>(HttpRequestMessage request)
    {
        var response = await _client.SendAsync(request);
        var raw = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (raw != "")
            {

                var data = JsonSerializer.Deserialize<T>(
                    raw,
                    new JsonSerializerOptions
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


    protected Task<ApiResultDto<T>>
        GetAsync<T>(string url) =>
        SendGetRequestAsync<T>(HttpMethod.Get, url);


    protected Task<ApiResultDto<T>>
        PostAsync<T>(string url, HttpContent content) =>
        SendPostRequestAsync<T>(url, content);

    protected Task<ApiResultDto<T>>
        PutAsync<T>(string url, HttpContent content) =>
        SendPutRequestAsync<T>(url, content);


    protected Task<ApiResultDto<T>>
        PatchAsync<T>(string url, HttpContent content) =>
        SendPatchRequestAsync<T>(url, content);


    protected Task<ApiResultDto<T>>
        DeleteAsync<T>(string url) =>
        SendDeleteRequestAsync<T>(url);

}
