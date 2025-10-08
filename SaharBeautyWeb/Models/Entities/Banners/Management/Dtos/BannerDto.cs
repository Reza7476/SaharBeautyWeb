namespace SaharBeautyWeb.Models.Entities.Banners.Management.Dtos;

public class BannerDto
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public long Id { get; set; }
    public string? ImageName { get; set; }
    public string? URL { get; set; }
    public string? CreateDate { get; set; }
    public string? Title { get; set; }
    public string? Error { get; set; }
}
