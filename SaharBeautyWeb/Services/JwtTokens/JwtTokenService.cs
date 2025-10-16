namespace SaharBeautyWeb.Services.JwtTokens;

public class JwtTokenService : IJwtTokenService
{

    string? IJwtTokenService.FirstName { get; set; }
    string? IJwtTokenService.LastName { get; set; }
    string? IJwtTokenService.UserId { get; set; }
    string? IJwtTokenService.Mobile { get; set; }
    string? IJwtTokenService.UserName { get; set; }
    DateTime ExpireDate { get; set; }
    DateTime IJwtTokenService.ExpireDate { get; set; }
    List<string> IJwtTokenService.Roles { get; set; } = new();

}