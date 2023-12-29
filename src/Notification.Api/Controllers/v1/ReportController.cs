using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Api.Models.v1.Report;
using Notification.Api.Queries.QueryHandlers.v1.Report.GetReportSms;

namespace Notification.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Sms")]
        public async Task<IActionResult> GetSms([FromQuery] GetReportSmsRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetReportSmsQueryRequest(request.From, request.To, request.ProjectIds), cancellationToken);
            return Ok(new GetReportSmsResponse(response.From, response.To, response.Values));
        }
    }
}
