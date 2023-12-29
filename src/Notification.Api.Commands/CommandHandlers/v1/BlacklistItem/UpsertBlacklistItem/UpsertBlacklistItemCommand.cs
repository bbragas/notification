using MediatR;

namespace Notification.Api.Commands.CommandHandlers.v1.BlacklistItem.UpsertBlacklistItem
{
    public record UpsertBlacklistItemCommand(
    Guid ProjectId,
    Guid? ProjectEntityId,
    string Contact,
    string Description) : IRequest<Unit>;
}
