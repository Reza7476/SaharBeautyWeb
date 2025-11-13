using SaharBeautyWeb.Configurations.Interfaces;
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.SMS_Logs.Dto;

namespace SaharBeautyWeb.Services.UserPanels.Admin.SMS_Logs;

public interface ISMSLogService : IService
{
    Task<ApiResultDto<GetAllDto<GetAllSentSMSDto>>>
        GetAllSentSMS(int limit, int offset, string? search = null);

    Task<ApiResultDto<GetSMSNumberCreditDto?>> GetCredit();
}
