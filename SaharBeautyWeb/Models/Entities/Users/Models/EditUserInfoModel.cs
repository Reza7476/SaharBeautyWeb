using SaharBeautyWeb.Models.Commons.Models;

namespace SaharBeautyWeb.Models.Entities.Users.Models;

public class EditUserInfoModel
{
    public string? Name { get; set; }
    public string? Mobile { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? CreationDate { get; set; }
    public string? BirthDate { get; set; }
    public ImageDetailsModel? Avatar { get; set; }
}
