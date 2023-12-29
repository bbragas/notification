using MediatR;
using Notification.Api.Gateway.Http.Whatsapp;
using Notification.Api.Infrastructure.Exceptions.Queries;

namespace Notification.Api.Queries.QueryHandlers.v1.Whatsapp.GetWhatsappNotSentQueueCountQuery
{
    public class GetNotSentQueueCountQueryHandler : IRequestHandler<GetNotSentQueueCountQueryRequest, GetNotSentQueueCountQueryResponse>
    {
        private readonly IWhatsappHttpClient _whatsappHttpClient;

        public GetNotSentQueueCountQueryHandler(IWhatsappHttpClient whatsappHttpClient)
        {
            _whatsappHttpClient = whatsappHttpClient;
        }

        public async Task<GetNotSentQueueCountQueryResponse> Handle(GetNotSentQueueCountQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _whatsappHttpClient.GetQueueNotSendCountAsync(cancellationToken);

            if (!response.Success)
                throw new WhatsappQueueNotSentException();
            return new GetNotSentQueueCountQueryResponse(response.Stats.Sending);
        }
    }
}
