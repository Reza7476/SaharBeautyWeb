namespace SaharBeautyWeb.Configurations.Extensions;

public static  class PaginationExtensions
{
    public static int ToOffset(this int pageNumber, int limit)
    {
        var offset = (pageNumber - 1) * limit;
        return offset;  
    }
}
