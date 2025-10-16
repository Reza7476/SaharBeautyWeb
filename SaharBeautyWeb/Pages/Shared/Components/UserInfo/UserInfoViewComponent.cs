using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Services.JwtTokens;

namespace SaharBeautyWeb.Pages.Shared.Components.UserInfo;

public class UserInfoViewComponent : ViewComponent
{
    private readonly IJwtTokenService _jwtService;

    public UserInfoViewComponent(IJwtTokenService jwtService)
    {
        _jwtService = jwtService;
    }
    public string? FullName { get; set; }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = new UserInfoViewComponentModel()
        {
            FullName=_jwtService.FirstName
        };

        return View("Default",vm);
    }
}

public class UserInfoViewComponentModel
{
    public string? FullName { get; set; } 
}