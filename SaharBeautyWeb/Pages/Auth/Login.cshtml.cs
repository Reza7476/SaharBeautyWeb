using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Entities.Auth;
using SaharBeautyWeb.Models.Entities.Auth.Dtos;
using SaharBeautyWeb.Services.AboutUs;
using SaharBeautyWeb.Services.Auth;

namespace SaharBeautyWeb.Pages.Auth;

public class LoginModel : PageModel
{
    private readonly IAboutUsService _aboutUsService;
    private readonly IAutheService _authService;

    public LoginModel(IAboutUsService aboutUsService,
        IAutheService authService)
    {
        _aboutUsService = aboutUsService;
        _authService = authService;
    }
    public LogoForLoginModel? Logo { get; set; }
    [BindProperty]
    public LoginDataModel DataModel { get; set; } = default!;
    public async Task OnGet()
    {
        var aboutUs = await _aboutUsService.GeAboutUs();

        if (aboutUs.IsSuccess && aboutUs.Data != null)
        {
            var logo = aboutUs.Data.LogoImage != null ?
                new LogoForLoginModel()
                {
                    ImageName = aboutUs.Data.LogoImage.ImageName,
                    URL = aboutUs.Data.LogoImage.Url,
                } : null;
        }
    }

    public async Task<IActionResult> OnPostLogin()
    {
        if (DataModel.UserName == null)
        {
            BadRequest();
        }
        if (DataModel.Password == null)
        {
            BadRequest();

        }

        var result = await _authService.LoginUser(new LoginDto()
        {
            Password = DataModel.Password!,
            UserName = DataModel.UserName!
        });
        if(result.IsSuccess && result.Data != null)
        {
            HttpContext.Session.SetString("JwtToken", result.Data.JwtToken!=null?result.Data.JwtToken:" ");
            HttpContext.Session.SetString("RefreshToken", result.Data.RefreshToken!=null?result.Data.RefreshToken:" ");
            return RedirectToPage("/UserPanels/Index");
        }


        return Page();
    }
}
