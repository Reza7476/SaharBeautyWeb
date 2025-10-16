using SaharBeautyWeb.Configurations.Interfaces;

namespace SaharBeautyWeb.Services.JwtTokens;

public interface IJwtTokenService : IService
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserId { get; set; }
    public string? Mobile { get; set; }
    public string? UserName { get; set; }
    public DateTime ExpireDate { get; set; }
    public List<string> Roles { get; set; } 



}
