using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Api.Commands.CommandHandlers.v1.BlacklistItem.RemoveBlacklistItem;
using Notification.Api.Messages.BlacklistEntity.DTOs;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;
using Notification.Api.Commands.CommandHandlers.v1.BlacklistItem.UpsertBlacklistItem;

namespace Notification.Api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]

public class BlacklistController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlacklistController(IMediator mediator)
        => _mediator = mediator;

    [HttpPut]
    public async Task<IActionResult> Upsert(BlacklistItemRequestDTO request, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new UpsertBlacklistItemCommand(request.ProjectId, request.ProjectEntityId, request.Contact, request.Description), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{blacklistItemId}")]
    public async Task<IActionResult> Remove([FromRoute] Guid blacklistItemId, CancellationToken cancellationToken = default)
    {
        if (blacklistItemId == Guid.Empty)
            throw new ArgumentException($"{nameof(blacklistItemId)} is invalid");

        await _mediator.Send(new RemoveBlacklistItemCommand(blacklistItemId), cancellationToken);
        return NoContent();
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllBlacklistDTO request, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetAllBlacklistQueryRequest(request.ProjectId, request.ProjectEntityId, default, request.PerPage, request.Page), cancellationToken));

}