namespace SaharBeautyWeb.Models.Entities.Auth.Dtos;

public class GetTokenDto
{
    public string? JwtToken { get; set; }
    public string? RefreshToken { get; set; }
}
