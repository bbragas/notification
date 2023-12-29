using MediatR;

namespace Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetBlacklistItemQuery
{
   public record GetBlacklistItemQueryRequest(Guid ProjectId, Guid? ProjectEntityId, string Contact, CancellationToken CancellationToken) : IRequest<GetBlacklistItemQueryResponse>;

   public record GetBlacklistItemQueryByIdRequest(Guid BlacklistItemId, CancellationToken CancellationToken) : IRequest<GetBlacklistItemQueryResponse>;
}
