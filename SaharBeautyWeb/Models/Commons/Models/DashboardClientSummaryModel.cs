namespace SaharBeautyWeb.Models.Commons.Models;

public class DashboardClientSummaryModel
{
    public List<DashboardClientAppointmentModel> FutureAppointments { get; set; } = new();
    public List<DashboardClientAppointmentModel> FormerAppointments { get; set; } = new();
}
public class DashboardClientAppointmentModel
{
    public string? TreatmentTitle { get; set; }
    public string? Day { get; set; }
    public string? Date { get; set; }
    public string? Start { get; set; }
    public string? Status { get; set; }
}
