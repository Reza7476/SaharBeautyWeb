using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons;

namespace SaharBeautyWeb.Services.Contracts;

public interface ICrudApiService : IService
{
    Task<ApiResultDto<T>> AddAsync<T>(string url, MultipartFormDataContent content);
    Task<ApiResultDto<T>> GetAsync<T>(string url);

}
