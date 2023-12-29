using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.Services;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Exceptions;
using Notification.Api.Infrastructure.Messages.v1;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;

namespace Notification.Api.Commands.CommandHandlers.v1.NotifySms;

public class NotifySmsCommandHandler : IRequestHandler<NotifySmsCommand, Unit>
{
    private readonly IWriteRepository _repository;
    private readonly ILogger<NotifySmsCommandHandler> _logger;
    private readonly IPublishService _publishService;
    private readonly IMediator _mediator;

    public NotifySmsCommandHandler(IWriteRepository repository,
                                    ILogger<NotifySmsCommandHandler> logger,
                                    IPublishService publishService,
                                    IMediator mediator)

    {
        _repository = repository;
        _logger = logger;
        _publishService = publishService;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(NotifySmsCommand request, CancellationToken cancellationToken)
    {
        if (request == default)
            return Unit.Value;

        var blacklist =
                await _mediator.Send(new GetAllBlacklistQueryRequest(request.ProjectId, request.ProjectEntityId, request.PhoneNumber, default, default), cancellationToken);
        if (blacklist.Total > 0)
        {
            _logger.LogDebug($"Sms notification was not send, because the contact is in blacklist. ProjectId: {request.ProjectId}, ProjectEntityId: {request.ProjectEntityId}, PhoneNumber: {request.PhoneNumber}.");
            throw new BlacklistItemFoundException(typeof(BlacklistEntity), request.PhoneNumber);
        }

        var sms = new Sms
        {
            Id = Guid.NewGuid(),
            Client = request.Client,
            Campaign = request.Campaign,
            ProjectId = request.ProjectId.ToString(),
            ProjectEntityId = request.ProjectEntityId.ToString(),
            Message = request.Message,
            SenderName = request.SenderName,
            PhoneNumber = request.PhoneNumber,
            ScheduledTo = request.ScheduledTo,
            ExternalId = request.ExternalId?.ToString(),
            Status = request.ScheduledTo.HasValue ? NotificationStatus.Scheduled : NotificationStatus.Fired
        };

        await _repository.CreateAsync(sms, cancellationToken);

        if (!sms.IsScheduled)
        {
            var sendSms = new SendSmsEvent(
                request.ExternalId ?? sms.Id,
                sms.Campaign,
                sms.SenderName,
                sms.PhoneNumber,
                sms.Message);

            await _publishService.Publish(sendSms, cancellationToken);

            _logger.LogDebug("Starting process to save SMS in mongo and to sent the SQS message.");
        }

        return Unit.Value;
    }
}

