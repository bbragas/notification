using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Notification.Api.Infrastructure.Messages;

namespace Notification.Api.Commands.Services;

public class SqsService : ISqsService
{
    private readonly IAmazonSQS _sqs;
    private readonly ILogger _logger;

    public SqsService(IAmazonSQS sqs, ILogger<SqsService> logger)
    {
        _sqs = sqs;
        _logger = logger;
    }

    public async Task SendMessageAsync<TMessage>(TMessage message, string queueUrl, CancellationToken cancellationToken)
    {
        if (object.Equals(message, default(TMessage)))
            return;

        var messageSerialized = JsonSerializer.Serialize(message);
        var messageRequest = new SendMessageRequest(queueUrl, messageSerialized);

        var result = await _sqs.SendMessageAsync(messageRequest, cancellationToken);

        var isSuccess = (int)result.HttpStatusCode >= 200 && (int)result.HttpStatusCode <= 299;
        if (!isSuccess)
            throw new HttpRequestException($"SQS could not be sent, error http : {result.HttpStatusCode}");

        _logger.LogDebug($"Sqs message to sent with success");
    }
}
