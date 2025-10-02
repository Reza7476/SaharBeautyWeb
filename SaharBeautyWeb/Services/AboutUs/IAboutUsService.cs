using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Dtos;

namespace SaharBeautyWeb.Services.AboutUs;

public interface IAboutUsService : IService
{
    Task<ApiResultDto<GetAboutUsDto?>> GeAboutUs();
    Task<ApiResultDto<long>> Add(AddAboutUsDto dto);
    Task<ApiResultDto<GetAboutUsDto?>> GeAboutUsById(long id);
    Task<ApiResultDto<object>> Edit(EditAboutUsDto dto);
}
