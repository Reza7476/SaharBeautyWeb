using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Models.Entities.SMS_Logs.Model;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.Admin.SMS_Logs;

namespace SaharBeautyWeb.Pages.UserPanels.Admin.SMS_Logs;

public class IndexModel : AjaxBasePageModel
{
    private readonly ISMSLogService _smsLogService;

    public IndexModel(ErrorMessages errorMessage, ISMSLogService smsLogService) : base(errorMessage)
    {
        _smsLogService = smsLogService;
    }

    public GetSMSNumberCreditModel? CreditModel { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public GetAllSMSModel? AllSMSModel { get; set; }
    public async Task<IActionResult> OnGet(int pageNumber = 0, int limit = 10)
    {

        var credit = await _smsLogService.GetCredit();

        if (credit.IsSuccess && credit.Data != null)
        {
            CreditModel = new GetSMSNumberCreditModel()
            {
                Amount = credit.Data.Amount,
                Message = credit.Data.Status
            };
        }
        else
        {
            var result = HandleApiResult(credit);
            return result;
        }
        int offset = pageNumber;

        if (!string.IsNullOrWhiteSpace(Search))
        {
            Search = Search.RemoveCountryCodeFromPhoneNumber();
        }
        var smsList = await _smsLogService.GetAllSentSMS(limit, offset, Search);
        if (smsList.IsSuccess && smsList.Data != null)
        {
            AllSMSModel = new GetAllSMSModel()
            {
                AllSMS = smsList.Data.Elements.Select(_ => new AllSMSModel()
                {
                    Content = _.Content,
                    ReceiverNumber = _.ReceiverNumber,
                    RecId = _.RecId,
                    ResponseContent = _.ResponseContent,
                    Title = _.Title,
                    Status = _.Status.ConvertSMSStatusToString(),
                    CreatedAt = _.CreatedAt.ConvertGregorianDateWithTimeToShamsi()
                }).ToList(),
                CurrentPage = pageNumber,
                TotalElements = smsList.Data.TotalElements,
                TotalPages = smsList.Data.TotalElements.ToTotalPage(limit),
            };
        }
        else
        {
            var response = HandleApiResult(smsList);

            return response;
        }

        return Page();
    }
}
