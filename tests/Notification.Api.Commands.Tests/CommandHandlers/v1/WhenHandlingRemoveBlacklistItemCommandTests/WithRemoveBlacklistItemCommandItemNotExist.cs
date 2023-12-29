using Microsoft.Extensions.Logging;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Exceptions;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using MediatR;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetBlacklistItemQuery;
using Notification.Api.Commands.CommandHandlers.v1.BlacklistItem.RemoveBlacklistItem;

namespace Notification.Api.Commands.Tests.CommandHandlers.v1.WhenHandlingRemoveBlacklistItemCommandTests;

[TestClass]
public class WithRemoveBlacklistItemCommandItemNotExist
{
    private readonly IWriteRepository _writeRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<RemoveBlacklistItemCommandHandler> _logger;
    private readonly RemoveBlacklistItemCommand _command;
    private readonly Func<Task> _act;

    public WithRemoveBlacklistItemCommandItemNotExist()
    {
        _writeRepository = Substitute.For<IWriteRepository>();
        _mediator = Substitute.For<IMediator>();
        _logger = Substitute.For<ILogger<RemoveBlacklistItemCommandHandler>>();

        _command = new RemoveBlacklistItemCommand(Guid.NewGuid());

        var stub = new RemoveBlacklistItemCommandHandler(_writeRepository, _logger, _mediator);
        _act = async () => await stub.Handle(_command, default);
    }

    [TestMethod]
    public async Task Should_Received_Send()
    {
        await _act.Should()
            .ThrowExactlyAsync<ResourceNotFoundException>()
            .WithMessage($"{nameof(BlacklistEntity)} not found");

        await _mediator.Received().Send(
            Arg.Any<GetBlacklistItemQueryByIdRequest>(),
        Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task Should_Not_Received_DeleteAsync_If_Resource_Not_Exist()
    {
        await _act.Should().
            ThrowExactlyAsync<ResourceNotFoundException>()
            .WithMessage($"{nameof(BlacklistEntity)} not found");

        await _writeRepository.DidNotReceive().DeleteAsync<BlacklistEntity>(
        Arg.Any<Guid>(),
        Arg.Any<CancellationToken>());
    }
}
