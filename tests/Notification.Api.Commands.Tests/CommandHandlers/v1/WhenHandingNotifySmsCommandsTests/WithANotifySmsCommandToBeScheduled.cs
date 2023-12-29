using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.CommandHandlers.v1.NotifySms;
using Notification.Api.Commands.Services;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Messages.v1;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Messages.BlacklistEntity.DTOs;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;

namespace Notification.Api.Commands.Tests.CommandHandlers.v1.WhenHandingNotifySmsCommandsTests;

[TestClass]
public class WithANotifySmsCommandToBeScheduled
{
    private readonly Fixture _fixture = new();

    private readonly IWriteRepository _repository;
    private readonly IPublishService _publishService;
    private readonly NotifySmsCommand _command;
    private readonly IMediator _mediator;
    private readonly PaginatedDTO<BlacklistItemResponseDTO> _response;

    public WithANotifySmsCommandToBeScheduled()
    {
        // Arrange
        var logger = Substitute.For<ILogger<NotifySmsCommandHandler>>();

        _publishService = Substitute.For<IPublishService>();
        _repository = Substitute.For<IWriteRepository>();
        _mediator = Substitute.For<IMediator>();
        _fixture.Customize<PaginatedDTO<BlacklistItemResponseDTO>>(f => f.With(t => t.Total, 0));
        _response = _fixture.Create<PaginatedDTO<BlacklistItemResponseDTO>>();
        _mediator.Send(Arg.Any<GetAllBlacklistQueryRequest>(), Arg.Any<CancellationToken>())
                .Returns(_response);
        _command = _fixture.Create<NotifySmsCommand>();

        // Act
        new NotifySmsCommandHandler(_repository, logger, _publishService, _mediator).Handle(_command, CancellationToken.None).Wait();
    }

    [TestMethod]
    public Task IEnvelopEventServiceShouldNotPublishAnyMessage()
    => _publishService.DidNotReceive().Publish(
        Arg.Any<SendSmsEvent>(),
        Arg.Any<CancellationToken>());

    [TestMethod]
    public Task IWriteRepositoryShouldReceivedASmslNotifictionToBeCreated()
    => _repository.Received().CreateAsync(
        Arg.Is<Sms>(p => AssertSms(p)),
        Arg.Any<CancellationToken>());

    [TestMethod]
    public Task IMediatorShouldValidateIfExistsBlacklistItemToSmsNotificationContact()
    => _mediator.Received().Send(
        Arg.Any<GetAllBlacklistQueryRequest>(),
        Arg.Any<CancellationToken>());

    private bool AssertSms(Sms domain)
    {
        domain.Campaign.Should().Be(_command.Campaign);
        domain.Client.Should().Be(_command.Client);
        domain.ProjectEntityId.Should().Be(_command.ProjectEntityId.ToString());
        domain.ProjectId.Should().Be(_command.ProjectId.ToString());
        domain.Message.Should().Be(_command.Message);
        domain.PhoneNumber.Should().Be(_command.PhoneNumber);

        return true;
    }
}

