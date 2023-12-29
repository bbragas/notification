using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetBlacklistItemQuery;

namespace Notification.Api.Commands.CommandHandlers.v1.BlacklistItem.UpsertBlacklistItem
{
    public class UpsertBlacklistItemCommandHandler : IRequestHandler<UpsertBlacklistItemCommand, Unit>
    {
        private readonly IWriteRepository _writeRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<UpsertBlacklistItemCommandHandler> _logger;

        public UpsertBlacklistItemCommandHandler(IWriteRepository writeRepository, IMediator mediator, ILogger<UpsertBlacklistItemCommandHandler> logger)
        {
            _writeRepository = writeRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpsertBlacklistItemCommand request, CancellationToken cancellationToken)
        {
            if (request == default)
                return Unit.Value;

            var blacklistItem =
                await _mediator.Send(new GetBlacklistItemQueryRequest(request.ProjectId, request.ProjectEntityId, request.Contact, cancellationToken));

            var upsertBlacklistItem = new Domain.BlacklistEntity
            {
                Id = blacklistItem?.Id ?? Guid.NewGuid(),
                ProjectId = request.ProjectId,
                ProjectEntityId = request.ProjectEntityId,
                Contact = request.Contact,
                Description = request.Description
            };

            await _writeRepository.UpsertAsync(upsertBlacklistItem, cancellationToken);

            _logger.LogDebug("Blacklist created with success! ProjectId:{0}, ProjectEndityId: {1}, Contact: {2}, Description: {3}", upsertBlacklistItem.ProjectId, upsertBlacklistItem.ProjectEntityId, upsertBlacklistItem.Contact, upsertBlacklistItem.Description);

            return Unit.Value;
        }
    }
}
