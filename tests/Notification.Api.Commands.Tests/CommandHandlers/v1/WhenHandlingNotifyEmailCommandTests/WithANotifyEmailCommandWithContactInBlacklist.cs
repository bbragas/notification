using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.CommandHandlers.v1.NotifyEmail;
using Notification.Api.Commands.Services;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Exceptions;
using Notification.Api.Infrastructure.Messages.v1;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Messages.BlacklistEntity.DTOs;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;

namespace Notification.Api.Commands.Tests.CommandHandlers.v1.WhenHandlingNotifyEmailCommandTests;

[TestClass]
public class WithANotifyEmailCommandWithContactInBlacklist
{
    private readonly Fixture _fixture = new();
    private readonly IWriteRepository _repository;
    private readonly IPublishService _publishService;
    private readonly NotifyEmailCommand _command;
    private readonly IMediator _mediator;
    private readonly Func<Task> _act;
    private readonly PaginatedDTO<BlacklistItemResponseDTO> _response;

    public WithANotifyEmailCommandWithContactInBlacklist()
    {
        // Arrange
        _publishService = Substitute.For<IPublishService>();
        _repository = Substitute.For<IWriteRepository>();
        _mediator = Substitute.For<IMediator>();
        var logger = Substitute.For<ILogger<NotifyEmailCommandHandler>>();

        _command = _fixture.Create<NotifyEmailCommand>();
        _response = _fixture.Create<PaginatedDTO<BlacklistItemResponseDTO>>();

        _mediator.Send(Arg.Any<GetAllBlacklistQueryRequest>(), Arg.Any<CancellationToken>())
                .Returns(_response);

        // Act
        var handler = new NotifyEmailCommandHandler(_repository, _publishService, logger, _mediator);
        _act = async () => await handler.Handle(_command, default);
    }

    [TestMethod]
    public Task IEnvelopEventServiceShouldNotPublishAnyMessage()
        => _publishService.DidNotReceive().Publish(
            Arg.Any<SendEmailEvent>(),
            Arg.Any<CancellationToken>());


    [TestMethod]
    public Task IWriteRepositoryShouldNotReceivedSendMessageBatchAsyncWithCorrectValues()
        => _repository.DidNotReceive().CreateAsync(
             Arg.Any<Email>(),
             Arg.Any<CancellationToken>());


    [TestMethod]
    public Task IMediatorShouldValidateIfExistsBlacklistItemToEmailNotificationContact()
        => _mediator.DidNotReceive().Send(
            Arg.Any<GetAllBlacklistQueryRequest>(),
            Arg.Any<CancellationToken>());


    [TestMethod]
    public Task ShouldLaunchException()
        => _act.Should()
               .ThrowExactlyAsync<BlacklistItemFoundException>()
               .WithMessage($"{nameof(BlacklistEntity)} found for contact {_command.RecipientEmail}");


}
