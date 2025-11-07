using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace SaharBeautyWeb.Models.Entities.Clients.Models;

public class AddNewClientModel
{

    [Required(ErrorMessage ="شماره تلفن مشتری را وارد کنید")]
    public string Mobile { get; set; } = default!;

    [Required(ErrorMessage ="نام مشتری را وارد کنید")]
    public string Name { get; set; } = default!;
    
    [Required(ErrorMessage ="نام خانوادگی مشتری را وارد کنید")]    
    public string LastName { get; set; } = default!;
}
