using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Api.Commands.CommandHandlers.v2.Whatsapp.NotifyWhatsapp;
using Notification.Api.Messages.Whatsapp.DTOs;
using Notification.Api.Queries.QueryHandlers.v1.Whatsapp.GetWhatsappNotSentQueueCountQuery;

namespace Notification.Api.Controllers.v2;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public class WhatsappController : ControllerBase
{
    private readonly IMediator _mediator;

    public WhatsappController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Queue")]
    public async Task<IActionResult> GetNotSentQueueCount(CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetNotSentQueueCountQueryRequest(), cancellationToken));
    
    [HttpPost()]
    public async Task<IActionResult> Notify([FromBody]NotifyWhatsappRequestDTO request, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(
            new NotifyWhatsappCommand(
                request.UnitId, 
                request.ProjecttId, 
                request.ProjectEntityId, 
                request.ProjectName, 
                request.Message, 
                request.MessageType, 
                request.Subtitle, 
                request.RecipientNumber, 
                request.ExternalId, 
                request.ScheduledTo) , cancellationToken));
    }
}
