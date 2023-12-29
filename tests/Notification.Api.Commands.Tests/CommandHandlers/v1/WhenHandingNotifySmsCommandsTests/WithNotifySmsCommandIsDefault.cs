using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.CommandHandlers.v1.NotifySms;
using Notification.Api.Commands.Services;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Messages.v1;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;
using NSubstitute;

namespace Notification.Api.Commands.Tests.CommandHandlers.v1.WhenHandingNotifySmsCommandsTests;

public class WithNotifySmsCommandIsDefault
{
    private readonly IWriteRepository _repository;
    private readonly IPublishService _publishService;
    private readonly IMediator _mediator;

    public WithNotifySmsCommandIsDefault()
    {
        // Arrange
        var logger = Substitute.For<ILogger<NotifySmsCommandHandler>>();

        _publishService = Substitute.For<IPublishService>();
        _repository = Substitute.For<IWriteRepository>();
        _mediator = Substitute.For<IMediator>();

        // Act
        new NotifySmsCommandHandler(_repository, logger, _publishService, _mediator).Handle(default, CancellationToken.None).Wait();
    }

    [TestMethod]
    public async Task IEnvelopEventServiceShouldNotPublishAnyMessage()
    => await _publishService.DidNotReceive().Publish(
        Arg.Any<SendSmsEvent>(),
        Arg.Any<CancellationToken>());

    [TestMethod]
    public async Task IWriteRepositoryShouldNotReceivedSendMessageBatchAsyncWithCorrectValues()
     => await _repository.DidNotReceive().CreateAsync(
             Arg.Any<Sms>(),
             Arg.Any<CancellationToken>());

    [TestMethod]
    public async Task IMediatorShouldNotValidateIfExistsBlacklistItemToSmsNotificationContact()
    => await _mediator.DidNotReceive().Send(
        Arg.Any<GetAllBlacklistQueryRequest>(),
        Arg.Any<CancellationToken>());
}

