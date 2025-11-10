using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaharBeautyWeb.Models.Entities.Auth;
using SaharBeautyWeb.Models.Entities.Auth.Dtos;
using SaharBeautyWeb.Services.Auth;
using SaharBeautyWeb.Services.Landing.AboutUs;
using SaharBeautyWeb.Services.UserPanels.LogOut;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SaharBeautyWeb.Pages.Auth;

public class LoginModel : PageModel
{
    private readonly IAboutUsService _aboutUsService;
    private readonly IAutheService _authService;
    private readonly ILogoutService _logOutService;
    public LoginModel(IAboutUsService aboutUsService,
        IAutheService authService,
        ILogoutService logOutService)
    {
        _aboutUsService = aboutUsService;
        _authService = authService;
        _logOutService = logOutService;
    }
    public LogoForLoginModel? Logo { get; set; }
    [BindProperty]
    public LoginDataModel DataModel { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string? ErrorMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }


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

        if (!string.IsNullOrWhiteSpace(Request.Query["errorMessage"]))
        {
            ErrorMessage = Request.Query["errorMessage"];
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
        if (result.IsSuccess && result.Data != null)
        {
            HttpContext.Session.Remove("JwtToken");
            HttpContext.Session.Remove("RefreshToken");
            HttpContext.Session.SetString(
                "JwtToken",
                result.Data.JwtToken != null ?
                result.Data.JwtToken : " ");
            HttpContext.Session.SetString(
                "RefreshToken",
                result.Data.RefreshToken != null ?
                result.Data.RefreshToken : " ");
            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }

            var jwtToken = result.Data.JwtToken;
            if (!string.IsNullOrWhiteSpace(jwtToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwtToken);
                var role = token.Claims.First(c => c.Type == ClaimTypes.Role ||
                                              c.Type == "role" ||
                                              c.Type == "Role")?.Value;

                if (!string.IsNullOrWhiteSpace(role))
                {
                    if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToPage("/UserPanels/Admin/Index");
                    }
                    if (role.Equals("Client", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToPage("/UserPanels/Client/Index");
                    }
                }
            }
        }
        ErrorMessage = result.Error ?? "ورود ناموفق دوباره تلاش کنید ";
        return Page();
    }

    public async Task<IActionResult> OnGetRemoveToken()
    {
        var token = HttpContext.Session.GetString("RefreshToken");
        if (token != null)
        {
            var result = await _logOutService.LogOutToken(token);
            if (result.IsSuccess)
            {
                HttpContext.Session.Remove("JwtToken");
                HttpContext.Session.Remove("RefreshToken");
                HttpContext.Session.Clear();
                return new JsonResult(new
                {
                    sussess = result.IsSuccess,
                    statusCode = result.StatusCode
                });
            }
            else
            {
                return new JsonResult(new
                {
                    success = result.IsSuccess,
                    error = result.Error,
                    statusCode = result.StatusCode
                });
            }
        }
        return new JsonResult(new
        {
            success = false,
        });
    }
}
