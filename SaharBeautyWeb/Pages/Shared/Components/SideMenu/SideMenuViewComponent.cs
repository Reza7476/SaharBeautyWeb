using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Models.Commons.Models;
using SaharBeautyWeb.Services.JwtTokens;
using SaharBeautyWeb.Services.UserPanels.Users;

namespace SaharBeautyWeb.Pages.Shared.Components.SideMenu;

public class SideMenuViewComponent : ViewComponent
{
    private readonly IJwtTokenService _jwtService;
    private readonly IUserService _userService;
    public SideMenuViewComponent(IJwtTokenService jwtService, IUserService userService)
    {
        _jwtService = jwtService;
        _userService = userService;
    }


    public UserRoleModel? userInfoModel { get; set; }
    public async Task<IViewComponentResult> InvokeAsync()
    {

        List<string> roles = new List<string>();
        string? name = " ";
        string? lastName = " ";
        ImageDetailsModel? avatar = new();
        var userInfo = await _userService.GetUserInfo();
        if (userInfo.IsSuccess && userInfo.Data != null)
        {
            name = userInfo.Data.Name;
            lastName = userInfo.Data.LastName;
            avatar = userInfo.Data.Avatar != null ? new ImageDetailsModel()
            {
                Extension = userInfo.Data.Avatar.Extension,
                ImageName = userInfo.Data.Avatar.ImageName,
                UniqueName = userInfo.Data.Avatar.UniqueName,
                Url = userInfo.Data.Avatar.Url
            } : null;

        }
        userInfoModel = new UserRoleModel()
        {
            Roles = _jwtService.Roles,
            FullName = name + " " + lastName,
            ImageURL = avatar != null ? avatar.Url : null
        };
        return View(userInfoModel);
    }
}

public class UserRoleModel
{
    public List<string> Roles { get; set; } = new();
    public string? FullName { get; set; }
    public string? ImageURL { get; set; }
}
