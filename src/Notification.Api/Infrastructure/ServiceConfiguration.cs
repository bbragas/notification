using MediatR;
using Notification.Api.Commands.CommandHandlers.v1.NotifyEmail;
using Notification.Api.Queries.QueryHandlers.v1.Report.GetReportSms;
using Notification.Api.Queries.QueryHandlers.v1.Whatsapp.GetWhatsappNotSentQueueCountQuery;

namespace Notification.Api.Infrastructure;

public static class ServiceConfiguration
{
    public static IServiceCollection AddComandAndQueryServices(this IServiceCollection services)
    {
        return services
                    .AddMediatR(typeof(NotifyEmailCommandHandler).Assembly, typeof(GetReportSmsQueryHandler).Assembly, typeof(GetNotSentQueueCountQueryHandler).Assembly);
    }
}
