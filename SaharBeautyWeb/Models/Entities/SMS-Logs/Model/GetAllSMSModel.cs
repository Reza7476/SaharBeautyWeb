namespace SaharBeautyWeb.Models.Entities.SMS_Logs.Model;

public class GetAllSMSModel
{
    public List<AllSMSModel> AllSMS { get; set; } = new();
    public int TotalElements { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
