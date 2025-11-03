using SaharBeautyWeb.Models.Commons.Models;

namespace SaharBeautyWeb.Models.Entities.Users.Models;

public class UserInfoModel
{
    public string Id { get; set; } = default!;
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? CreationDate { get; set; }
    public string? BirthDate { get; set; }
    public bool IsActive { get; set; }
    public ImageDetailsModel? Avatar { get; set; }
    public List<String> RoleNames { get; set; } = default!;
}
