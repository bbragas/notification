using Amazon.SQS;
using Amazon.SQS.Model;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.Services;
using Notification.Api.Infrastructure.Messages;
using System.Text.Json;

namespace Notification.Api.Commands.Tests.Services.SqsServiceTests;
public class WhenCallSendMessageAsyncAndReturnSuccessCode
{
    private readonly Fixture _fixture = new();

    private readonly IAmazonSQS _sqs;
    private readonly Envelop _message;
    private readonly string _queueName;
    private readonly Func<Task> _act;

    public WhenCallSendMessageAsyncAndReturnSuccessCode()
    {
        // Arrange
        _sqs = Substitute.For<IAmazonSQS>();

        _message = _fixture.Create<Envelop>();
        _queueName = _fixture.Create<string>();

        var logger = Substitute.For<ILogger<SqsService>>();
        var service = new SqsService(_sqs, logger);

        _sqs.SendMessageAsync(Arg.Any<SendMessageRequest>(), Arg.Any<CancellationToken>())
            .Returns(new SendMessageResponse
            {
                HttpStatusCode = System.Net.HttpStatusCode.OK
            });

        // Act
        _act = () => service.SendMessageAsync(_message, _queueName, default);
    }

    [TestMethod]
    public async Task ShouldCallSendMessageAsyncCorrctely()
    {
        // Assert
        var messageSerialized = JsonSerializer.Serialize(_message);

        await _sqs.Received().SendMessageAsync(
            Arg.Is<SendMessageRequest>(p => p.QueueUrl == _queueName && p.MessageBody == messageSerialized),
            Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task ShouldNotThrowHttpRequestException()
        => await _act.Should().NotThrowAsync<HttpRequestException>();
}
