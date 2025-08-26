using Microsoft.AspNetCore.Mvc;

namespace SaharBeautyWeb.Pages.Shared.Components.Notification;

public class NotificationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
