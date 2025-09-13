namespace SaharBeautyWeb.Models.Commons.Dtos;

public class ViewComponentErrorDto
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; }
}
