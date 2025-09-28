using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;

namespace SaharBeautyWeb.Services.Contracts;

public interface ICRUDApiService : IService
{
    Task<ApiResultDto<T>> AddFromFormAsync<T>(string url, MultipartFormDataContent content);
    Task<ApiResultDto<object>> Delete<T>(string url);
    Task<ApiResultDto<GetAllDto<T>>> GetAllAsync<T>(string url, int? offset = null, int? limit = null);

    Task<ApiResultDto<T>> GetAsync<T>(string url);

    Task<ApiResultDto<T>> UpdateAsPatchAsync<T>(string url, MultipartFormDataContent content);
    Task<ApiResultDto<object>> UpdateAsPutFromBodyAsync<T>(string url, HttpContent content);
    Task<ApiResultDto<T>> AddFromBodyAsync<T>(string url, HttpContent content);
}
