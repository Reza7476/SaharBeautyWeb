using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Services.UserPanels.Users;

namespace SaharBeautyWeb.Pages.Shared.Components.UserInfo;

public class UserInfoViewComponent : ViewComponent
{
    private readonly IUserService _userService;

    public UserInfoViewComponent(
        IUserService userService)
    {
        _userService = userService;
    }
    public string? FullName { get; set; }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = new UserInfoViewComponentModel();

        var userInfo = await _userService.GetUserInfo();
        if (userInfo.IsSuccess && userInfo.Data != null)
        {
            vm.FullName = userInfo.Data.Name + " " + userInfo.Data.LastName;
            vm.ImageURL = userInfo.Data.Avatar != null ? userInfo.Data.Avatar.Url : null;
        }
        return View("Default", vm);
    }
}

public class UserInfoViewComponentModel
{
    public string? FullName { get; set; }
    public string? ImageURL { get; set; }
}