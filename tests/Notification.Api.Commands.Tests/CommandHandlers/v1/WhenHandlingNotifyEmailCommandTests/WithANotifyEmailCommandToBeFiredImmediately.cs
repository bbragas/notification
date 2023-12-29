﻿using AutoFixture;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.Services;
using Notification.Api.Infrastructure.Messages.v1;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using MediatR;
using Notification.Api.Domain;
using Notification.Api.Messages.BlacklistEntity.DTOs;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;
using Notification.Api.Commands.CommandHandlers.v1.NotifyEmail;

namespace Notification.Api.Commands.Tests.CommandHandlers.v1.WhenHandlingNotifyEmailCommandTests;

[TestClass]
public class WithANotifyEmailCommandToBeFiredImmediately
{
    private readonly Fixture _fixture = new();

    private readonly IWriteRepository _repository;
    private readonly IPublishService _publishService;
    private readonly NotifyEmailCommand _command;
    private readonly PaginatedDTO<BlacklistItemResponseDTO> _response;
    private readonly IMediator _mediator;

    public WithANotifyEmailCommandToBeFiredImmediately()
    {
        // Arrange
        _publishService = Substitute.For<IPublishService>();
        _repository = Substitute.For<IWriteRepository>();
        _mediator = Substitute.For<IMediator>();
        var logger = Substitute.For<ILogger<NotifyEmailCommandHandler>>();

        _fixture.Customize<PaginatedDTO<BlacklistItemResponseDTO>>(f => f.With(t => t.Total, 0));
        _response = _fixture.Create<PaginatedDTO<BlacklistItemResponseDTO>>();

        _mediator.Send(Arg.Any<GetAllBlacklistQueryRequest>(), Arg.Any<CancellationToken>())
                .Returns(_response);
        _command = _fixture.Build<NotifyEmailCommand>()
            .With(x => x.ScheduledTo, () => default)
            .Create();

        // Act
        new NotifyEmailCommandHandler(_repository, _publishService, logger, _mediator).Handle(_command, CancellationToken.None).Wait();
    }

    [TestMethod]
    public Task IEnvelopEventServiceShouldPublishAMessageWithCorrectValues()
        => _publishService.Received().Publish(
            Arg.Is<SendEmailEvent>(p => AssertEmailEvent(p)),
            Arg.Any<CancellationToken>());

    [TestMethod]
    public Task IWriteRepositoryShouldReceivedAnEmailNotifictionToBeCreated()
     => _repository.Received().CreateAsync(
             Arg.Is<Email>(p => AssertEmail(p)),
             Arg.Any<CancellationToken>());


    private bool AssertEmail(Email domain)
    {
        domain.Body.Should().Be(_command.Body);
        domain.Campaign.Should().Be(_command.Campaign);
        domain.Client.Should().Be(_command.Client);
        domain.ProjectEntityId.Should().Be(_command.ProjectEntityId);
        domain.ProjectId.Should().Be(_command.ProjectId);
        domain.RecipientEmail.Should().Be(_command.RecipientEmail);
        domain.RecipientName.Should().Be(_command.RecipientName);
        domain.SenderEmail.Should().Be(_command.SenderEmail);
        domain.SenderName.Should().Be(_command.SenderName);
        domain.Subject.Should().Be(_command.Subject);

        return true;
    }

    private bool AssertEmailEvent(SendEmailEvent @event)
    {
        @event.Id.Should().NotBeEmpty();
        @event.To.Should().Be(_command.RecipientEmail);
        @event.Name.Should().Be(_command.RecipientName);
        @event.Subject.Should().Be(_command.Subject);
        @event.SenderName.Should().Be(_command.SenderName);
        @event.Html.Should().Be(_command.Body);

        return true;
    }

    [TestMethod]
    public Task IMediatorShouldValidateIfExistsBlacklistItemToEmailNotificationContact()
        => _mediator.Received().Send(
            Arg.Any<GetAllBlacklistQueryRequest>(),
            Arg.Any<CancellationToken>());
}
