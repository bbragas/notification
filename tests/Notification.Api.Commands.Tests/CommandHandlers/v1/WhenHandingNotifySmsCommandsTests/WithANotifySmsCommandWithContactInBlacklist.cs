using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.CommandHandlers.v1.NotifySms;
using Notification.Api.Commands.Services;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Exceptions;
using Notification.Api.Infrastructure.Messages.v1;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Messages.BlacklistEntity.DTOs;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetBlacklistItemQuery;

namespace Notification.Api.Commands.Tests.CommandHandlers.v1.WhenHandingNotifySmsCommandsTests;

[TestClass]
public class WithANotifySmsCommandWithContactInBlacklist
{
    private readonly Fixture _fixture = new();

    private readonly IWriteRepository _repository;
    private readonly IPublishService _publishService;
    private readonly NotifySmsCommand _command;
    private readonly IMediator _mediator;
    private readonly Func<Task> _act;
    private readonly PaginatedDTO<BlacklistItemResponseDTO> _response;

    public WithANotifySmsCommandWithContactInBlacklist()
    {
        // Arrange
        var logger = Substitute.For<ILogger<NotifySmsCommandHandler>>();

        _publishService = Substitute.For<IPublishService>();
        _repository = Substitute.For<IWriteRepository>();
        _mediator = Substitute.For<IMediator>();
        _command = _fixture.Create<NotifySmsCommand>();
        _response = _fixture.Create<PaginatedDTO<BlacklistItemResponseDTO>>();
        _mediator.Send(Arg.Any<GetAllBlacklistQueryRequest>(), Arg.Any<CancellationToken>())
                .Returns(_response);

        // Act
        var handler = new NotifySmsCommandHandler(_repository, logger, _publishService, _mediator);
        _act = async () => await handler.Handle(_command, default);
    }

    [TestMethod]
    public Task IEnvelopEventServiceShouldNotPublishAnyMessage()
        => _publishService.DidNotReceive().Publish(
            Arg.Any<SendSmsEvent>(),
            Arg.Any<CancellationToken>());

    [TestMethod]
    public Task IWriteRepositoryShouldNotReceivedSendMessageBatchAsyncWithCorrectValues()
        => _repository.DidNotReceive().CreateAsync(
             Arg.Any<Sms>(),
             Arg.Any<CancellationToken>());


    [TestMethod]
    public Task IMediatorShouldValidateIfExistsBlacklistItemToSmsNotificationContact()
        => _mediator.DidNotReceive().Send(
            Arg.Any<GetAllBlacklistQueryRequest>(),
            Arg.Any<CancellationToken>());


    [TestMethod]
    public Task ShouldLaunchException()
    => _act.Should()
        .ThrowExactlyAsync<BlacklistItemFoundException>()
        .WithMessage($"{nameof(BlacklistEntity)} found for contact {_command.PhoneNumber}");

}
