using Microsoft.AspNetCore.Mvc;

namespace SaharBeautyWeb.Pages.Shared.Components.DarkMode;

public class DarkModeViewComponent:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
