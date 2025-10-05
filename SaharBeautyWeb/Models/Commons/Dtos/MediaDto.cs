using System.Text.Json.Serialization;

namespace SaharBeautyWeb.Models.Commons.Dtos;

public class MediaDto
{
    public string? UniqueName { get; set; }
    public string? ImageName { get; set; }
    public string? Extension { get; set; }
    public string? URL { get; set; }
    public long Id { get; set; }
}
