using Microsoft.AspNetCore.Mvc;

namespace SaharBeautyWeb.Pages.Shared.Components.UserInfo;

public class UserInfoViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
