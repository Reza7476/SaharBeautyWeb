
using SaharBeautyWeb.Services.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAuthService _authService;
    public JwtAuthMiddleware(
        RequestDelegate next,
        IAuthService authService)
    {
        _next = next;
        _authService = authService;
    }

    private static bool IsLocalPath(string? path)
    {
        if (string.IsNullOrEmpty(path)) return false;
        return path.StartsWith("/") && !path.StartsWith("//") && !path.StartsWith("/\\") && !path.Contains("://");
    }

    private static async Task HandleUnauthenticatedAsync(HttpContext context, string returnUrl, string? message = null)
    {
        var isAjax = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        if (isAjax)
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("SessionExpired");
        }
        else
        {
            var encoded = Uri.EscapeDataString(IsLocalPath(returnUrl) ? returnUrl : "/");
            var redirectUrl = $"/Auth/Login?returnUrl={encoded}";
            if (!string.IsNullOrEmpty(message))
                redirectUrl += $"&errorMessage={Uri.EscapeDataString(message)}";
            context.Response.Redirect(redirectUrl);
        }
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        if (path.StartsWith("/auth", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }



        if (path.StartsWith("/userpanels", StringComparison.OrdinalIgnoreCase))
        {
            var token = context.Session.GetString("JwtToken");
            var refreshToken = context.Session.GetString("RefreshToken");
            var returnUrl = (context.Request.Path + context.Request.QueryString).ToString();

            if (!IsLocalPath(returnUrl))
            {
                returnUrl = "/";
            }
            var encodedReturnUrl = Uri.EscapeDataString(returnUrl);

            if (string.IsNullOrWhiteSpace(token) ||
                string.IsNullOrWhiteSpace(refreshToken))
            {
                await HandleUnauthenticatedAsync(context, returnUrl, "نیاز به ورود مجدد دارید");
                return;
            }


            var jwt = ValidateAndExtractJwt(token);
            if (jwt == null)
            {
                await HandleUnauthenticatedAsync(context, returnUrl, "توکن نامعتبر است");
                return;
            }



            var exp = jwt!.ValidTo; // UTC
            var timeRemaining = exp - DateTime.UtcNow;

            if (timeRemaining <= TimeSpan.FromMinutes(1))
            {
                try
                {
                    var refreshResult = await _authService.RefreshToken(refreshToken);

                    if (refreshResult.IsSuccess && refreshResult.Data != null)
                    {
                        context.Session.SetString("JwtToken", refreshResult.Data.JwtToken ?? string.Empty);
                        context.Session.SetString("RefreshToken", refreshResult.Data.RefreshToken ?? string.Empty);

                        await _next(context);
                        return;
                    }
                    else
                    {
                        var msg = refreshResult.Error ?? "تمدید توکن ممکن نبود";
                        await HandleUnauthenticatedAsync(context, returnUrl, refreshResult.Error ?? "تمدید توکن ممکن نبود");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    await HandleUnauthenticatedAsync(context, returnUrl, "خطای شبکه هنگام تمدید توکن");
                    return;
                }
            }

            if (!HasValidForPath(jwt, path, context))
            {
                context.Response.Redirect("/Auth/AccessDenied");
                return;
            }
        }

        await _next(context);
    }

    private static bool HasValidForPath(
        JwtSecurityToken jwt,
        string path,
        HttpContext context)
    {
        var result = true;
        var roles = jwt.Claims
                   .Where(c => c.Type == ClaimTypes.Role ||
                   c.Type.EndsWith("/claims/role"))
                   .Select(c => c.Value)
                   .ToList();
        if (path.StartsWith("/userpanels/admin",
            StringComparison.OrdinalIgnoreCase) &&
            !roles.Contains("Admin"))
        {
            context.Response.Redirect("/Auth/AccessDenied");
            result = false;
        }

        if (path.StartsWith("/userpanels/client",
            StringComparison.OrdinalIgnoreCase) &&
            !roles.Contains("Client"))
        {
            context.Response.Redirect("/Auth/AccessDenied");
            result = false;
        }
        return result;
    }

    private static JwtSecurityToken? ValidateAndExtractJwt(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }
        catch
        {
            return null;
        }
    }
}