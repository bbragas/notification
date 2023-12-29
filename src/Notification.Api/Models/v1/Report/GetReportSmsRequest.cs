using Microsoft.AspNetCore.Mvc;

namespace Notification.Api.Models.v1.Report;

public class GetReportSmsRequest
{
    [FromQuery(Name = "projectIds")]
    public string[] ProjectIds { get; set; } = Array.Empty<string>();
    [FromQuery(Name = "from")]
    public DateTime From { get; set; } = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
    [FromQuery(Name = "to")]
    public DateTime To { get; set; } = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1);
}
