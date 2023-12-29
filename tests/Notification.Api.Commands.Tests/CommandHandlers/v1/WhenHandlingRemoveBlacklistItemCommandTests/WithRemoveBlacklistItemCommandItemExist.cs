using Microsoft.Extensions.Logging;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using NSubstitute.Extensions;
using MediatR;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetBlacklistItemQuery;
using Notification.Api.Commands.CommandHandlers.v1.BlacklistItem.RemoveBlacklistItem;

namespace Notification.Api.Commands.Tests.CommandHandlers.v1.WhenHandlingRemoveBlacklistItemCommandTests;

[TestClass]
public class WithRemoveBlacklistItemCommandItemExist
{
    private readonly IWriteRepository _writeRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<RemoveBlacklistItemCommandHandler> _logger;
    private readonly RemoveBlacklistItemCommand _command;

    public WithRemoveBlacklistItemCommandItemExist()
    {
        _mediator = Substitute.For<IMediator>();
        _writeRepository = Substitute.For<IWriteRepository>();
        _logger = Substitute.For<ILogger<RemoveBlacklistItemCommandHandler>>();
        _command = new RemoveBlacklistItemCommand(Guid.NewGuid());

        GetBlacklistItemQueryResponse fakeResult = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            string.Empty,
            string.Empty);

        _mediator
            .Configure()
            .Send(Arg.Any<GetBlacklistItemQueryByIdRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(fakeResult));

        new RemoveBlacklistItemCommandHandler(_writeRepository, _logger, _mediator)
            .Handle(_command, CancellationToken.None).Wait();
    }

    [TestMethod]
    public async Task Should_Received_Send()
    {
        await _mediator.Received().Send(
            Arg.Any<GetBlacklistItemQueryByIdRequest>(),
        Arg.Any<CancellationToken>());
    }

    [TestMethod]

    public async Task Should_Received_DeleteAsync()
    {
        await _writeRepository.Received().DeleteAsync<BlacklistEntity>(
        Arg.Any<Guid>(),
        Arg.Any<CancellationToken>());
    }
}
