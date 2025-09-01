namespace SaharBeautyWeb.Models.Commons;

public class ViewComponentErrorDto
{
    public bool IsSuccess { get; set; }
    public string? Description { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; }
}
