using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Exceptions;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetBlacklistItemQuery;

namespace Notification.Api.Commands.CommandHandlers.v1.BlacklistItem.RemoveBlacklistItem
{
    public class RemoveBlacklistItemCommandHandler : IRequestHandler<RemoveBlacklistItemCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IWriteRepository _writeRepository;
        private readonly ILogger<RemoveBlacklistItemCommandHandler> _logger;

        public RemoveBlacklistItemCommandHandler(IWriteRepository writeRepository, ILogger<RemoveBlacklistItemCommandHandler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _writeRepository = writeRepository;
        }

        public async Task<Unit> Handle(RemoveBlacklistItemCommand request, CancellationToken cancellationToken)
        {
            var blacklistItem = await _mediator.Send(new GetBlacklistItemQueryByIdRequest(request.BlacklistItemId, cancellationToken), cancellationToken);

            if (blacklistItem is null) throw new ResourceNotFoundException(typeof(BlacklistEntity));

            await _writeRepository.DeleteAsync<BlacklistEntity>(blacklistItem.Id, cancellationToken);

            _logger.LogDebug("Blacklist item removed with success! Id: {id}", blacklistItem.Id);

            return Unit.Value;
        }
    }
}
