using SaharBeautyWeb.Models.Commons;
using System.Text.Json;

namespace SaharBeautyWeb.Services.Contracts;

public class CrudApiService : ICrudApiService
{

    private readonly HttpClient _client;

    public CrudApiService(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public async Task<ApiResultDto<T>> AddAsync<T>(string url, MultipartFormDataContent content)
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
    public async Task<ApiResultDto<object>> UpdateAsPutAsyncFromBody<T>(string url, HttpContent content)
    {
        var response = await _client.PutAsync(url, content);
        var raw= await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return new ApiResultDto<object> { 
                IsSuccess = response.IsSuccessStatusCode ,
                StatusCode=(int)response.StatusCode 
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

    public async Task<ApiResultDto<T>> GetAsync<T>(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);
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
                        IsSuccess = true,
                        Data = data,
                        StatusCode = (int)response.StatusCode
                    };
                }
                catch
                {
                    return new ApiResultDto<T>
                    {
                        IsSuccess = false,
                        Data = default,
                        StatusCode = (int)response.StatusCode,
                        Error = "پاسخ نامعتبر از سرور"
                    };
                }
            }
            return new ApiResultDto<T>
            {
                IsSuccess = false,
                StatusCode = (int)response.StatusCode,
                Error = !string.IsNullOrEmpty(raw) ? raw : response.ReasonPhrase
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

    public async Task<ApiResultDto<List<T>>> GetAllAsync<T>(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);
            var raw = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResultDto<List<T>>
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = (int)response.StatusCode,
                    Error = !string.IsNullOrEmpty(raw) ? raw : response.ReasonPhrase
                };
            }

            var wrapper = JsonSerializer.Deserialize<ApiListResponse<T>>(raw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new ApiResultDto<List<T>>
            {
                Data = wrapper?.Elements ?? new List<T>(),
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };
        }
        catch (Exception ex)
        {
            return new ApiResultDto<List<T>>
            {
                IsSuccess = false,
                Error = $"خطا در ارتباط با سرور: {ex.Message}"
            };
        }
    }


    public class ApiListResponse<T>
    {
        public List<T> Elements { get; set; } = default!;
        public int TotalElements { get; set; }
    }

    public class ErrorResponse
    {
        public string Error { get; set; }
        public int StatusCode { get; set; }
    }
}

