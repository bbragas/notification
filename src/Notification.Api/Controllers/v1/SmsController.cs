using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notification.Api.Models.v1.Sms;
using Notification.Api.Commands.CommandHandlers.v1.NotifySms;

namespace Notification.Api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class SmsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SmsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SmsSimpleNotify(NotifySmsDTO dto, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(_mapper.Map<NotifySmsCommand>(dto), cancellationToken);
        return NoContent();
    }
}

