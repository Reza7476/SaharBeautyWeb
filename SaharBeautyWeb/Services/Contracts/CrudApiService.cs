using SaharBeautyWeb.Models.Commons.Dtos;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SaharBeautyWeb.Services.Contracts;

public class CrudApiService : ICRUDApiService
{

    private readonly HttpClient _client;

    public CrudApiService(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public async Task<ApiResultDto<object>> Delete<T>(string url)
    {
        try
        {
            var response = await _client.DeleteAsync(url);
            var raw = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new ApiResultDto<object>
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = (int)response.StatusCode
                };
            }
            var data = JsonSerializer.Deserialize<ErrorResponse>(raw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return new ApiResultDto<object>
            {
                IsSuccess = response.IsSuccessStatusCode,
                Error = data?.Error ?? " An error has been accorded.",
                StatusCode = (int)response.StatusCode
            };

        }
        catch (Exception ex)
        {

            return new ApiResultDto<object>
            {
                IsSuccess = false,
                Error = ex.Message,
            };
        }
    }
    public async Task<ApiResultDto<T>> AddFromFormAsync<T>(string url, MultipartFormDataContent content)
    {
        var response = await _client.PostAsync(url, content);
        var raw = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var data = JsonSerializer.Deserialize<T>(raw, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return new ApiResultDto<T>
                {
                    Data = data,
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = (int)response.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ApiResultDto<T>
                {
                    IsSuccess = false,
                    Data = default,
                    StatusCode = (int)response.StatusCode,
                    Error = ex.Message
                };
            }
        }
        return new ApiResultDto<T>
        {
            IsSuccess = response.IsSuccessStatusCode,
            Error = string.IsNullOrWhiteSpace(raw).ToString()
        };
    }
    public async Task<ApiResultDto<object>> UpdateAsPutAsyncFromBody<T>(string url, HttpContent content)
    {
        var response = await _client.PutAsync(url, content);
        var raw = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return new ApiResultDto<object>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };
        }

        var data = JsonSerializer.Deserialize<ErrorResponse>(raw, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return new ApiResultDto<object>
        {
            IsSuccess = response.IsSuccessStatusCode,
            Error = data?.Error ?? " An error has been accorded.",
            StatusCode = (int)response.StatusCode
        };
    }

    public async Task<ApiResultDto<T>> AddFromBodyAsync<T>(string url, HttpContent content)
    {
        var response = await _client.PostAsync(url, content);
        var raw = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var data = JsonSerializer.Deserialize<T>(raw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return new ApiResultDto<T>
            {
                Data = data,
                IsSuccess=response.IsSuccessStatusCode,
                StatusCode=(int)response.StatusCode
            };
        }
        return new ApiResultDto<T>()
        {
            Error=raw,
            IsSuccess = response.IsSuccessStatusCode,
            StatusCode = (int)response.StatusCode
        };
    }

    public async Task<ApiResultDto<T>> GetAsync<T>(string url)
    {

        var response = await _client.GetAsync(url);
        var raw = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode || raw == "")
        {
            return new ApiResultDto<T>
            {
                IsSuccess = raw == "" ? true : false,
                Data = default,
                StatusCode = (int)response.StatusCode,
                Error = raw == "" ? null : raw
            };
        }

        var data = JsonSerializer.Deserialize<T>(raw, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return new ApiResultDto<T>
        {
            IsSuccess = true,
            Data = data,
            StatusCode = (int)response.StatusCode
        };
    }

    public async Task<ApiResultDto<T>> UpdateAsPatchAsync<T>(string url, MultipartFormDataContent content)
    {
        try
        {
            using var response = await _client.PatchAsync(url, content);
            var raw = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new ApiResultDto<T>
                {
                    IsSuccess = true,
                    StatusCode = (int)response.StatusCode
                };
            }

            return new ApiResultDto<T>
            {
                IsSuccess = false,
                StatusCode = (int)response.StatusCode,
                Error = !string.IsNullOrWhiteSpace(raw) ? raw : response.ReasonPhrase
            };
        }
        catch (Exception ex)
        {
            return new ApiResultDto<T>
            {
                IsSuccess = false,
                Error = $"خطا در ارتباط با سرور: {ex.Message}"
            };
        }
    }

    public async Task<ApiResultDto<GetAllDto<T>>> GetAllAsync<T>(string url, int? offset = null, int? limit = null)
    {
        try
        {
            var queryParams = new List<string>();
            if (offset.HasValue) queryParams.Add($"Offset={offset.Value}");
            if (limit.HasValue) queryParams.Add($"Limit={limit.Value}");
            if (queryParams.Any())
            {
                url += "?" + string.Join("&", queryParams);
            }

            var response = await _client.GetAsync(url);
            var raw = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResultDto<GetAllDto<T>>
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = (int)response.StatusCode,
                    Error = !string.IsNullOrEmpty(raw) ? raw : response.ReasonPhrase
                };
            }

            var wrapper = JsonSerializer.Deserialize<GetAllDto<T>>(raw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new ApiResultDto<GetAllDto<T>>
            {
                Data = wrapper,
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode,
            };
        }
        catch (Exception ex)
        {
            return new ApiResultDto<GetAllDto<T>>
            {
                IsSuccess = false,
                Error = $"خطا در ارتباط با سرور: {ex.Message}"
            };
        }
    }


    public class ErrorResponse
    {
        public string Error { get; set; }
        public int StatusCode { get; set; }
    }
}

