using SaharBeautyWeb.Models.Entities.SMS_Logs.Enum;

namespace SaharBeautyWeb.Models.Entities.SMS_Logs.Model;

public class AllSMSModel
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string ReceiverNumber { get; set; } = default!;
    public long RecId { get; set; }
    public string Status { get; set; } = default!;
    public string? ResponseContent { get; set; }
    public string CreatedAt { get; set; } = default!;
}
