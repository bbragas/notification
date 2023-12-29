using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notification.Api.Gateway.Http.Models;
using Notification.Api.Gateway.Http.Whatsapp;
using Notification.Api.Infrastructure.Exceptions.Queries;
using Notification.Api.Queries.QueryHandlers.v1.Whatsapp.GetWhatsappNotSentQueueCountQuery;
using NSubstitute;

namespace Notification.Api.Queries.Tests.QueryHandler.v1.Whatsapp.WhenGetNotSentQueueCountQuery;

[TestClass]
public class WithGetNotSentQueueCountQueryFailed
{
    private readonly IWhatsappHttpClient _whatsappHttpervice;
    private readonly QueueNotSentCountDto _providerResponse =
        new QueueNotSentCountDto()
        {
            Success = false,
            Stats = new StatsDto()
            {
                Sending = 0
            }
        };
    private GetNotSentQueueCountQueryResponse? _response;
    private readonly Func<Task> _act;

    public WithGetNotSentQueueCountQueryFailed()
    {
        _whatsappHttpervice = Substitute.For<IWhatsappHttpClient>();
        _whatsappHttpervice.GetQueueNotSendCountAsync(CancellationToken.None)
                .Returns(_providerResponse);

        var stub = new GetNotSentQueueCountQueryHandler(_whatsappHttpervice);
        _act = async () => await stub.Handle(new GetNotSentQueueCountQueryRequest(), default);
    }

    [TestMethod]
    public void Should_Received_Get_With_Failed()
          => _whatsappHttpervice.DidNotReceive()
             .GetQueueNotSendCountAsync(CancellationToken.None);

    [TestMethod]
    public async Task Should_Throw_Exception()
    {
        await _act.Should()
            .ThrowExactlyAsync<WhatsappQueueNotSentException>()
            .WithMessage("Error when try to get not sent Whatsapp messages count from provider queue.");
    }
}
