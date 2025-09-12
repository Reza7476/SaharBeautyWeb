using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons;

namespace SaharBeautyWeb.Services.Contracts;

public interface ICrudApiService : IService
{
    Task<ApiResultDto<T>> AddAsync<T>(string url, MultipartFormDataContent content);
    Task<ApiResultDto<object>> Delete<T>(string url);
    Task<ApiResultDto<List<T>>> GetAllAsync<T>(string url);
    
    Task<ApiResultDto<T>> GetAsync<T>(string url);

    Task<ApiResultDto<T>> UpdateAsync<T>(string url, MultipartFormDataContent content);
}
