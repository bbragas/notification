using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.CommandHandlers.v1.BlacklistItem.UpsertBlacklistItem;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetBlacklistItemQuery;

namespace Notification.Api.Commands.Tests.CommandHandlers.v1.WhenHandlingUpsertBlacklistItemCommandTests;

[TestClass]
public class WithUpsertBlacklistItemCommandDefault
{
    private readonly IWriteRepository _writeRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<UpsertBlacklistItemCommandHandler> _logger;

    public WithUpsertBlacklistItemCommandDefault()
    {
        _writeRepository = Substitute.For<IWriteRepository>();
        _mediator = Substitute.For<IMediator>();
        _logger = Substitute.For<ILogger<UpsertBlacklistItemCommandHandler>>();

        new UpsertBlacklistItemCommandHandler(_writeRepository, _mediator, _logger).Handle(default!, CancellationToken.None).Wait();
    }

    [TestMethod]
    public async Task Should_Not_Received_GetSingleOrDefaultByFilterAsync()
    {
        await _mediator.DidNotReceive().Send(Arg.Any<GetBlacklistItemQueryRequest>());
    }

    [TestMethod]
    public async Task Should_Not_Received_UpsertAsync()
    {
        await _writeRepository.DidNotReceive().UpsertAsync(
        Arg.Any<BlacklistEntity>(),
        Arg.Any<CancellationToken>());
    }
}
