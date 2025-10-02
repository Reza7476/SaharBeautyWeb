namespace SaharBeautyWeb.Models.Entities.AboutUs.Management.Dtos;

public class EditAboutUsDto
{
    public long Id { get; set; }
    public required string MobileNumber { get; set; }
    public string? Telephone { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? Email { get; set; }
    public string? Instagram { get; set; }
}
