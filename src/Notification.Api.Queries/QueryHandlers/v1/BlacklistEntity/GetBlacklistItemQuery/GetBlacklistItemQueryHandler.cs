using MediatR;
using Notification.Api.Infrastructure.Repository.Mongo.Base;

namespace Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetBlacklistItemQuery
{
    public class GetBlacklistItemQueryHandler :
        IRequestHandler<GetBlacklistItemQueryRequest, GetBlacklistItemQueryResponse>,
        IRequestHandler<GetBlacklistItemQueryByIdRequest, GetBlacklistItemQueryResponse>
    {
        private readonly IReadRepository _repository;

        public GetBlacklistItemQueryHandler(IReadRepository repository)
            => _repository = repository;

        public async Task<GetBlacklistItemQueryResponse> Handle(GetBlacklistItemQueryRequest request, CancellationToken cancellationToken)
        {
            var blacklistItem = await _repository.GetSingleOrDefaultByFilterAsync<Domain.BlacklistEntity>(req =>
                   req.ProjectId.Equals(request.ProjectId) &&
                   req.Contact.Equals(request.Contact) &&
                   req.ProjectEntityId.Equals(request.ProjectEntityId),
                   cancellationToken);

            return blacklistItem is null ? default : new(
                blacklistItem.Id,
                blacklistItem.ProjectId,
                blacklistItem.ProjectEntityId,
                blacklistItem.Contact,
                blacklistItem.Description);
        }

        public async Task<GetBlacklistItemQueryResponse> Handle(GetBlacklistItemQueryByIdRequest request, CancellationToken cancellationToken)
        {
            var blacklistItem = await _repository
                .GetSingleOrDefaultByFilterAsync<Domain.BlacklistEntity>(req => req.Id.Equals(request.BlacklistItemId), cancellationToken);

            return blacklistItem is null ? default : new(
                blacklistItem.Id,
                blacklistItem.ProjectId,
                blacklistItem.ProjectEntityId,
                blacklistItem.Contact,
                blacklistItem.Description);
        }
    }
}
