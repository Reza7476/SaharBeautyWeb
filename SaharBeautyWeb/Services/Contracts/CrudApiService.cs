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
        try
        {
            var response = await _client.PostAsync(url, content);
            var raw = await response.Content.ReadAsStringAsync();

            // حالت موفق: فقط عدد یا داده ساده
            if (typeof(T) == typeof(long) && long.TryParse(raw, out var id))
            {
                return new ApiResultDto<T>
                {
                    IsSuccess = true,
                    Data = (T)(object)id,
                    StatusCode = (int)response.StatusCode
                };
            }

            if (typeof(T) == typeof(string))
            {
                return new ApiResultDto<T>
                {
                    Data = (T)(object)raw,
                    IsSuccess = true,
                    StatusCode = (int)response.StatusCode
                };
            }

            // تلاش برای deserialize JSON (خطا یا داده پیچیده)
            var result = JsonSerializer.Deserialize<ApiResultDto<T>>(raw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result != null) return result;

            return new ApiResultDto<T>
            {
                IsSuccess = false,
                Error = "پاسخ نامعتبر از سرور",
                StatusCode = (int)response.StatusCode
            };
        }
        catch (Exception ex)
        {
            return new ApiResultDto<T>
            {
                IsSuccess = false,
                Error = $"خطا در ارتباط یا پردازش پاسخ: {ex.Message}",
                StatusCode = 0
            };
        }
    }

    public async Task<ApiResultDto<T>> GetAsync<T>(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);
            var raw = await response.Content.ReadAsStringAsync();

            // موفقیت: داده یا null
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
                        IsSuccess = true,
                        Data = default,
                        StatusCode = (int)response.StatusCode,
                        Error = "پاسخ نامعتبر از سرور"
                    };
                }
            }

            // حالت خطا: error + status code
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

}

