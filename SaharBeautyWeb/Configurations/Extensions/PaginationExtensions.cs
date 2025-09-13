namespace SaharBeautyWeb.Configurations.Extensions;

public static  class PaginationExtensions
{
    public static int ToOffset(this int pageNumber, int limit)
    {
        var offset = (pageNumber - 1) * limit;
        return offset;  
    }

    public static int ToTotalPage(this int totalElement, int limit)
    {
        var totalPage = (int)Math.Ceiling((double)totalElement/limit);
        return totalPage;
    }
}
