using MediatR;

namespace Notification.Api.Queries.QueryHandlers.v1.Report.GetReportSms;

public record GetReportSmsQueryRequest(DateTime From, DateTime To, string[] projectIds) : IRequest<GetReportSmsQueryResponse>;
