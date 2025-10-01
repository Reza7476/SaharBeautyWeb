using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.AboutUs.Management.Dtos;

namespace SaharBeautyWeb.Services.AboutUs;

public interface IAboutUsService : IService
{
    Task<ApiResultDto<GetAboutUsDto?>> GeAboutUs();
    Task<ApiResultDto<long>> Add(AddAboutUsDto dto);
}
