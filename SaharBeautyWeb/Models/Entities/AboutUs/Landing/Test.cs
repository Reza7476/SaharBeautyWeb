//using SaharBeautyWeb.Services.Auth;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;

//public class JwtAuthMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly IAutheService _authService;

//    public JwtAuthMiddleware(RequestDelegate next, IAutheService authService)
//    {
//        _next = next;
//        _authService = authService;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        var path = context.Request.Path.Value ?? string.Empty;
//        if (path.StartsWith("/auth", StringComparison.OrdinalIgnoreCase))
//        {
//            await _next(context);
//            return;
//        }
//


//        if (path.StartsWith("/userpanels", StringComparison.OrdinalIgnoreCase))
//        {
//            var returnUrl = (context.Request.Path + context.Request.QueryString).ToString();
//            var token = context.Session.GetString("JwtToken");
//            var refreshToken = context.Session.GetString("RefreshToken");
//            if (!IsLocalPath(returnUrl))
//            {
//                returnUrl = "/";
//            }

//            var encodedReturnUrl = Uri.EscapeDataString(returnUrl);
//
//           if (string.IsNullOrWhiteSpace(token) ||
//                string.IsNullOrWhiteSpace(refreshToken))
//            {
//                await HandleUnauthenticatedAsync(context, returnUrl, "نیاز به ورود مجدد دارید");
//                return;
//            }

//            var jwt = ValidateAndExtractJwt(token);

//            if (jwt == null)
//            {
//                await HandleUnauthenticatedAsync(context, returnUrl, "توکن نا معتبر است");
//                return;
//            }

//            if (!await TryRefreshTokenIfNeeded(context, jwt, refreshToken, token, returnUrl))
//                return;

//            if (!HasValidRoleForPath(jwt, path))
//            {
//                context.Response.Redirect("/Auth/AccessDenied");
//                return;
//            }
//            await _next(context);
//        }
//        await _next(context);
//    }

//    private static bool IsLocalPath(string? path)
//    {
//        if (string.IsNullOrEmpty(path)) return false;
//        return path.StartsWith("/") &&
//               !path.StartsWith("//") &&
//               !path.StartsWith("/\\") &&
//               !path.Contains("://");
//    }

//    //private static async Task HandleUnauthenticatedAsync(
//    //    HttpContext context,
//    //    string returnUrl,
//    //    string? message = null)
//    //{
//    //    var isAjax = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
//    //    if (isAjax)
//    //    {
//    //        context.Response.StatusCode = 401; // Unauthorized
//    //        await context.Response.WriteAsync("SessionExpired");
//    //    }
//    //    else
//    //    {
//    //        var encoded = Uri.EscapeDataString(IsLocalPath(returnUrl) ? returnUrl : "/");
//    //        var redirectUrl = $"/Auth/Login?returnUrl={encoded}";
//    //        if (!string.IsNullOrEmpty(message))
//    //            redirectUrl += $"&errorMessage={Uri.EscapeDataString(message)}";
//    //        context.Response.Redirect(redirectUrl);
//    //    }
//    //}

//    private static async Task HandleUnauthenticatedAsync(
//    HttpContext context,
//    string returnUrl,
//    string? message = null)
//    {
//        var isAjax = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

//        if (isAjax)
//        {
//            context.Response.StatusCode = 401; // Unauthorized
//            context.Response.ContentType = "application/json";
//            await context.Response.WriteAsync("SessionExpired");
//            return;
//        }

//        var encoded = Uri.EscapeDataString(IsLocalPath(returnUrl) ? returnUrl : "/");
//        var redirectUrl = $"/Auth/Login?returnUrl={encoded}";
//        if (!string.IsNullOrEmpty(message))
//            redirectUrl += $"&errorMessage={Uri.EscapeDataString(message)}";

//        context.Response.Redirect(redirectUrl);
//    }

//    private static JwtSecurityToken? ValidateAndExtractJwt(string token)
//    {
//        try
//        {
//            var handler = new JwtSecurityTokenHandler();
//            return handler.ReadJwtToken(token);
//        }
//        catch
//        {
//            return null;
//        }
//    }

//    private async Task<bool> TryRefreshTokenIfNeeded(
//       HttpContext context,
//       JwtSecurityToken jwt,
//       string refreshToken,
//       string token,
//       string returnUrl)
//    {
//        var exp = jwt.ValidTo;
//        var timeRemaining = exp - DateTime.UtcNow;

//        if (timeRemaining > TimeSpan.FromMinutes(1))
//            return true;

//        try
//        {
//            var refreshResult = await _authService
//                .RefreshToken(refreshToken, token);

//            if (refreshResult.IsSuccess &&
//                refreshResult.Data != null)
//            {
//                context.Session.SetString(
//                    "JwtToken",
//                    refreshResult.Data.JwtToken ??
//                    string.Empty);
//                context.Session.SetString(
//                    "RefreshToken",
//                    refreshResult.Data.RefreshToken ??
//                    string.Empty);
//                return true;
//            }

//            await HandleUnauthenticatedAsync(context, returnUrl, "تمدید توکن ممکن نبود");
//            return false;
//        }
//        catch
//        {
//            await HandleUnauthenticatedAsync(context, returnUrl, "خطای شبکه هنگام تمدید توکن");
//            return false;
//        }
//    }

//    private static bool HasValidRoleForPath(JwtSecurityToken jwt, string path)
//    {
//        var roles = jwt.Claims
//            .Where(c => c.Type == ClaimTypes.Role || c.Type.EndsWith("/claims/role"))
//            .Select(c => c.Value)
//            .ToList();

//        if (path.StartsWith("/userpanels/admin"))
//            return roles.Contains("Admin");

//        if (path.StartsWith("/userpanels/client"))
//            return roles.Contains("Client") || roles.Contains("User");

//        return true;
//    }
//}