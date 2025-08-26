using Microsoft.AspNetCore.Mvc;

namespace SaharBeautyWeb.Pages.Shared.Components.Footer;

public class FooterViewComponent:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
