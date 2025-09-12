namespace SaharBeautyWeb.Models.Commons.Dtos;

public class AddMediaDto
{
    public IFormFile AddMedia { get; set; } = default!;
    public long  Id { get; set; }
}
