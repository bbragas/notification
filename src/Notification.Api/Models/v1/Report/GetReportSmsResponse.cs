using Notification.Api.Queries.QueryHandlers.v1.Report.GetReportSms;

namespace Notification.Api.Models.v1.Report;
public record struct GetReportSmsResponse(DateTime From,
                                        DateTime To,
                                        IEnumerable<GetReportSmsItem> Values);