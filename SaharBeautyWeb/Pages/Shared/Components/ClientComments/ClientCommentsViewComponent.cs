using Microsoft.AspNetCore.Mvc;
using SaharBeautyWeb.Pages.Shared.Components.LandingBaseComponent;

namespace SaharBeautyWeb.Pages.Shared.Components.ClientComments;

public class ClientCommentsViewComponent : LandingBaseViewComponent
{
    public ClientCommentsViewComponent(ErrorMessages errorMessage) : base(errorMessage)
    {
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }

}
