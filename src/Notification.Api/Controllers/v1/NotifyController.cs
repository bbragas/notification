using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Api.Commands.CommandHandlers.v1.NotifyEmail;
using Notification.Api.Models.v1.Email;

namespace Notification.Api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Obsolete("Should use the specific API for you type of message.")]
public class NotifyController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public NotifyController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Notify(NotifyEmailDTO dto, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(_mapper.Map<NotifyEmailCommand>(dto), cancellationToken);
        return NoContent();
    }
}
