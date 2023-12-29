using Microsoft.Extensions.Logging;
using Notification.Api.Commands.Services;
using Notification.Api.Infrastructure.Messages.v1;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using MediatR;
using Notification.Api.Domain;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;
using Notification.Api.Commands.CommandHandlers.v1.NotifyEmail;

namespace Notification.Api.Commands.Tests.CommandHandlers.v1.WhenHandlingNotifyEmailCommandTests;

[TestClass]
public class WithNotifyEmailCommandIsDefault
{

    private readonly IWriteRepository _repository;
    private readonly IPublishService _publishService;
    private readonly IMediator _mediator;

    public WithNotifyEmailCommandIsDefault()
    {
        // Arrange
        _publishService = Substitute.For<IPublishService>();
        _repository = Substitute.For<IWriteRepository>();
        _mediator = Substitute.For<IMediator>();
        var logger = Substitute.For<ILogger<NotifyEmailCommandHandler>>();

        // Act
        new NotifyEmailCommandHandler(_repository, _publishService, logger, _mediator).Handle(default, CancellationToken.None).Wait();
    }

    [TestMethod]
    public async Task IEnvelopEventServiceShouldNotPublishAnyMessage()
        => await _publishService.DidNotReceive().Publish(
            Arg.Any<SendEmailEvent>(),
            Arg.Any<CancellationToken>());

    [TestMethod]
    public async Task IWriteRepositoryShouldNotReceivedSendMessageBatchAsyncWithCorrectValues()
     => await _repository.DidNotReceive().CreateAsync(
             Arg.Any<Email>(),
             Arg.Any<CancellationToken>());

    [TestMethod]
    public async Task IMediatorShouldNotValidateIfExistsBlacklistItemToEmailNotificationContact()
        => await _mediator.DidNotReceive().Send(
            Arg.Any<GetAllBlacklistQueryRequest>(),
            Arg.Any<CancellationToken>());
}
