using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.Services;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Exceptions;
using Notification.Api.Infrastructure.Messages.v1;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;

namespace Notification.Api.Commands.CommandHandlers.v1.NotifyEmail;

public class NotifyEmailCommandHandler : IRequestHandler<NotifyEmailCommand, Unit>
{
    private readonly IWriteRepository _repository;
    private readonly ILogger<NotifyEmailCommandHandler> _logger;
    private readonly IPublishService _publishService;
    private readonly IMediator _mediator;

    public NotifyEmailCommandHandler(
        IWriteRepository repository,
        IPublishService publishService,
        ILogger<NotifyEmailCommandHandler> logger,
        IMediator mediator)
    {
        _repository = repository;
        _logger = logger;
        _publishService = publishService;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(NotifyEmailCommand request, CancellationToken cancellationToken)
    {
        if (request == default)
            return Unit.Value;

        var blacklist =
                await _mediator.Send(new GetAllBlacklistQueryRequest(request.ProjectId, request.ProjectEntityId, request.RecipientEmail, default, default), cancellationToken);
        if (blacklist.Total > 0)
        {
            _logger.LogDebug("E-mail notification was not send, because the contact is in blacklist. ProjectId: {projectId}, ProjectEntityId: {projectEntityId}, RecipientEmail: {recipientEmail}.", request.ProjectId, request.ProjectEntityId, request.RecipientEmail);
            throw new BlacklistItemFoundException(typeof(BlacklistEntity), request.RecipientEmail);
        }

        var email = new Email
        {
            Client = request.Client,
            Campaign = request.Campaign,
            ProjectId = request.ProjectId,
            ProjectEntityId = request.ProjectEntityId,
            SenderName = request.SenderName,
            SenderEmail = request.SenderEmail,
            RecipientName = request.RecipientName,
            RecipientEmail = request.RecipientEmail,
            Subject = request.Subject,
            Body = request.Body,
            ScheduledTo = request.ScheduledTo,
            ExternalId = request.ExternalId,
            Status = request.ScheduledTo.HasValue ? NotificationStatus.Scheduled : NotificationStatus.Fired
        };

        await _repository.CreateAsync(email, cancellationToken);

        if (!email.IsScheduled)
        {
            var sendEmail = new SendEmailEvent(
                email.ExternalId ?? email.Id,
                email.RecipientEmail,
                email.RecipientName,
                email.Subject,
                email.SenderName,
                email.Body);

            await _publishService.Publish(sendEmail, cancellationToken);

            _logger.LogDebug("Starting process to save email in mongo and to sent the SQS message.");
        }

        return Unit.Value;
    }
}