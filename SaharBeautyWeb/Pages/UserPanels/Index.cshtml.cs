using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Pages.Shared;
using SaharBeautyWeb.Services.UserPanels.UserFCMTokens;

namespace SaharBeautyWeb.Pages.UserPanels;

public class IndexModel : AjaxBasePageModel
{

    private readonly IUserFCMTokenService _userFCMTokenService;
    public IndexModel(
        ErrorMessages errorMessage,
        IUserFCMTokenService userFCMTokenService) : base(errorMessage)
    {
        _userFCMTokenService = userFCMTokenService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostSendFireBaseToken(string token)
    {

        if (token != null)
        {
            var result = await _userFCMTokenService.Add(token);
            var response = HandleApiAjxResult(result);
            return response;
        }
        return null;
    }
}
