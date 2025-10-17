using SaharBeautyWeb.Services.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace SaharBeautyWeb.ProjectMiddleware;

public class JwtAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAutheService _authService;

    public JwtAuthMiddleware(RequestDelegate next,
        IAutheService authService)
    {
        _next = next;
        _authService = authService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();
        if (path != null && path.StartsWith("/userpanels"))
        {
            var token = context.Session.GetString("JwtToken");
            var refreshToken = context.Session.GetString("RefreshToken");
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(refreshToken))
            {
                context.Response.Redirect("/Auth/Login?returnUrl=" + path);
                return;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var exp = jwt.ValidTo;

            if (exp <= DateTime.UtcNow.AddMinutes(1))
            {
                if (string.IsNullOrWhiteSpace(refreshToken))
                {
                    context.Response.Redirect("/Auth/Login?returnUrl=" + path);
                    return;
                }
                var newToken = await _authService.RefreshToken(refreshToken, token);

                if (newToken.IsSuccess && newToken.Data != null)
                {
                    context.Session.Clear();
                    context.Session.SetString("JwtToken", newToken.Data.JwtToken!);
                    context.Session.SetString("RefreshToken", newToken.Data.RefreshToken!);
                    await _next(context);
                    return;
                }
                else
                {
                    context.Response.Redirect("/Auth/Login?expired=true");
                    return;
                }
            }
            await _next(context);
            return;
        }
        await _next(context);
    }
}
