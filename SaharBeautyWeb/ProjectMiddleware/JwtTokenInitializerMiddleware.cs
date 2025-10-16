using SaharBeautyWeb.Services.JwtTokens;
using System.IdentityModel.Tokens.Jwt;

namespace SaharBeautyWeb.ProjectMiddleware;

public class JwtTokenInitializerMiddleware
{
    private readonly RequestDelegate _next;

    public JwtTokenInitializerMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext context, IJwtTokenService jwtTokenService)
    {
        var token = context.Session.GetString("JwtToken");
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            jwtTokenService.UserId = jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            jwtTokenService.UserName = jwt.Claims.FirstOrDefault(c => c.Type == "UniqueName")?.Value;
            jwtTokenService.FirstName = jwt.Claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;
            jwtTokenService.LastName = jwt.Claims.FirstOrDefault(c => c.Type == "LastName")?.Value;
            jwtTokenService.Mobile = jwt.Claims.FirstOrDefault(c => c.Type == "Mobile")?.Value;

            var exp = jwt.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
            if (long.TryParse(exp, out var expUnix))
                jwtTokenService.ExpireDate = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
            jwtTokenService.Roles = jwt.Claims
                .Where(c => c.Type == "role" || c.Type.EndsWith("/claims/role"))
                .Select(c => c.Value)
                .ToList();
        }

        await _next(context);
    }
}
