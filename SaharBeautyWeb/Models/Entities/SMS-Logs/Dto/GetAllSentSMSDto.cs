using SaharBeautyWeb.Models.Entities.SMS_Logs.Enum;

namespace SaharBeautyWeb.Models.Entities.SMS_Logs.Dto;

public class GetAllSentSMSDto
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string ReceiverNumber { get; set; } = default!;
    public long RecId { get; set; }
    public SendSMSStatus Status { get; set; }
    public string? ResponseContent { get; set; }
    public DateTime CreatedAt { get; set; }
}
