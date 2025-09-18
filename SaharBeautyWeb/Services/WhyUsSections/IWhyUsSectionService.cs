using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Dtos;
using SaharBeautyWeb.Models.Entities.WhyUsSections.Models;

namespace SaharBeautyWeb.Services.WhyUsSections;

public interface IWhyUsSectionService : IService
{
    Task <ApiResultDto<long>> AddWhyUsQuestions(AddWhyUsQuestionsDto dto);
    Task<ApiResultDto<long>> AddWhyUsSection(AddWhyUsSectionDto dto);
    Task<ApiResultDto<object>> DeleteQuestion(long questionId);
    
    Task<ApiResultDto<object>> 
        EditTitleAndDescription(EditWhyUsSectionTitleAndDescriptionDto dto);
    
    Task<ApiResultDto<GetWhyUsSectionDto>> GetWhyUsSection();
    Task<ApiResultDto<WhyUsSectionModel_Edit>> GetWhyUsSectionById(long id);
}
