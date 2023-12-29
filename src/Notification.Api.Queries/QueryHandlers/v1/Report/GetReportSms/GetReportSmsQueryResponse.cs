namespace Notification.Api.Queries.QueryHandlers.v1.Report.GetReportSms;

public record GetReportSmsQueryResponse(DateTime From, DateTime To, IEnumerable<GetReportSmsItem> Values);
public record class GetReportSmsItem(string ProjectId, string Client, string Campaign)
{
    public int Total { get; set; } = 0;
}
