
using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Entities.SMS_Logs.Dto;
using SaharBeautyWeb.Models.Entities.Users.Dtos;

namespace SaharBeautyWeb.Services.UserPanels.Admin.SMS_Logs;

public class SMSLogService : UserPanelBaseService, ISMSLogService
{

    private const string _apiUrl = "sms";
    public SMSLogService(HttpClient client) : base(client)
    {
    }

    public async Task<ApiResultDto<GetAllDto<GetAllSentSMSDto>>> 
        GetAllSentSMS(int limit,int offset,string? search=null)
    {
        var url = $"{_apiUrl}/all-sent-sms";
        var query = new List<string>()
        {
            $"Offset={offset}",
            $"Limit={limit}",
        };

        if (!string.IsNullOrWhiteSpace(search))
        {
            query.Add($"search={search}");
        }

        if (query.Any())
        {
            url = url + "?" + string.Join("&", query);
        }


        var result = await GetAsync<GetAllDto<GetAllSentSMSDto>>(url);
        return result;
    }

    public async Task<ApiResultDto<GetSMSNumberCreditDto?>> GetCredit()
    {
        var url = $"{_apiUrl}/credit-number-of-sms";
        var result = await GetAsync<GetSMSNumberCreditDto?>(url);
        return result;
    }
}
