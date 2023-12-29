using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notification.Api.Gateway.Http.Models;
using Notification.Api.Gateway.Http.Whatsapp;
using Notification.Api.Queries.QueryHandlers.v1.Whatsapp.GetWhatsappNotSentQueueCountQuery;
using NSubstitute;

namespace Notification.Api.Queries.Tests.QueryHandler.v1.Whatsapp.WhenGetNotSentQueueCountQuery;

[TestClass]
public class WithGetNotSentQueueCountQuerySuccess
{
    private readonly IWhatsappHttpClient _whatsappHttpervice;
    private readonly QueueNotSentCountDto _providerResponse =
        new QueueNotSentCountDto()
        {
            Success = true,
            Stats = new StatsDto()
            {
                Sending = 10
            }
        };
    private GetNotSentQueueCountQueryResponse _response;

    public WithGetNotSentQueueCountQuerySuccess()
    {
        _whatsappHttpervice = Substitute.For<IWhatsappHttpClient>();
        _whatsappHttpervice.GetQueueNotSendCountAsync(CancellationToken.None)
                .Returns(_providerResponse);

        _response = new GetNotSentQueueCountQueryHandler(_whatsappHttpervice).Handle(new GetNotSentQueueCountQueryRequest(), CancellationToken.None).Result;
    }

    [TestMethod]
    public void Should_Received_Get_With_Success()
           => _whatsappHttpervice.Received()
              .GetQueueNotSendCountAsync(CancellationToken.None);

    [TestMethod]
    public void Should_Received_Get_With_Response_Not_Null()
    {
        Assert.IsNotNull(_response);
        Assert.AreEqual(_response.Sending, 10);
    }
}
