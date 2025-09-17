using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;

namespace SaharBeautyWeb.Services.WhyUsSections;

public interface IWhyUsSectionService : IService
{
    Task <ApiResultDto<long>> AddWhyUsQuestions(AddWhyUsQuestionsDto dto);
    Task<ApiResultDto<long>> AddWhyUsSection(AddWhyUsSectionDto dto);
    Task<ApiResultDto<object>> DeleteQuestion(long questionId);
    Task<ApiResultDto<GetWhyUsSectionDto>> GetWhyUsSection();
}
