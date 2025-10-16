using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Services.JwtTokens;

namespace SaharBeautyWeb.Pages.Shared.Components.SideMenu;

public class SideMenuViewComponent : ViewComponent
{
    private readonly IJwtTokenService _jwtService;

    public SideMenuViewComponent(IJwtTokenService jwtService)
    {
        _jwtService = jwtService;
    }

    public IViewComponentResult Invoke()
    {

        List<string> roles = new List<string>();
        return View(new UserRoleModel()
        {
            Roles = _jwtService.Roles,
        });
    }
}

public class UserRoleModel
{
    public List<string> Roles { get; set; } = new();
}