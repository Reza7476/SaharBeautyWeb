using SaharBeautyWeb.Models.Commons.Dtos;
using SaharBeautyWeb.Models.Commons.Models;

namespace SaharBeautyWeb.Models.Entities.Users.Models;

public class GetAllUsersModel
{


    public List<AllUsersModel> AllUsers { get; set; } = new();
    public int TotalElements { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

}
public class AllUsersModel
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<string> Roles { get; set; } = new();
    public string IsActive { get; set; } = default!;
    public string? CreatedAt { get; set; }
    public int AppointmentNumber { get; set; }
    public ImageDetailsModel? Avatar { get; set; }
}