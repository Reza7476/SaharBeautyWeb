namespace SaharBeautyWeb.Models.Commons.Dtos;

public class GetAllDto<T>
{
    public List<T> Elements { get; set; } = new List<T>();
    public int TotalElements { get; set; }
}
