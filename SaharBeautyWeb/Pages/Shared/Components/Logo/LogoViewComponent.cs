using Microsoft.AspNetCore.Mvc;

namespace SaharBeautyWeb.Pages.Shared.Components.Logo;

public class LogoViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();  
    }
}
