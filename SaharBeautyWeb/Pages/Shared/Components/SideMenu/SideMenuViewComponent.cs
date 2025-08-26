using Microsoft.AspNetCore.Mvc;

namespace SaharBeautyWeb.Pages.Shared.Components.SideMenu;

public class SideMenuViewComponent:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
