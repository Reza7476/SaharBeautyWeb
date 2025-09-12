using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Configurations.Extensions;

public  static class CheckImageFormatAndSize
{

    public static (bool isValid,string message) ValidateImage(this IFormFile? file)
    {
        if(file==null|| file.Length == 0)
        {
            return (false, "file is not selected");
        }

        var maxSize = 5 * 1024 * 1024;

        if(file.Length > maxSize)
        {
            return (false, "image size must be less than 5 MG");
        }

        var extension=Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!(extension == ".jpg" || extension == ".png" || extension == ".jpeg"))
        {
            return (false, "The image format is invalid");
        }
        return (true, " ");
    } 
}
