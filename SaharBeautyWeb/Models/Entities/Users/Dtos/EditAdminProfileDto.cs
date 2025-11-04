namespace SaharBeautyWeb.Models.Entities.Users.Dtos;

public class EditAdminProfileDto
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime? BirthDateGregorian { get; set; }
}
