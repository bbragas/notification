using MediatR;

namespace Notification.Api.Commands.CommandHandlers.v1.BlacklistItem.RemoveBlacklistItem;

public record RemoveBlacklistItemCommand(Guid BlacklistItemId) : IRequest<Unit>;