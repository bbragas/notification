using System.Text.Json;
using AutoFixture;
using Microsoft.Extensions.Options;
using Notification.Api.Commands.Services;
using Notification.Api.Commands.Settings;
using Notification.Api.Infrastructure.Messages;

namespace Notification.Api.Commands.Tests.Services.PunlishServiceTests;
public class WhenPublishANotificationEvent
{
    protected static readonly Fixture _fixture = new();

    private readonly ISqsService _sqs;
    private readonly NotifySettings _settings;
    private readonly INotificationEvent _notificationEvent;
    private Envelop _envelopUsed;

    public WhenPublishANotificationEvent()
    {
        // Arrange
        _settings = _fixture.Create<NotifySettings>();
        _notificationEvent = Substitute.For<INotificationEvent>();
        _notificationEvent.ToData().Returns(Guid.NewGuid().ToString());

        _sqs = Substitute.For<ISqsService>();
        var settingsOptions = Substitute.For<IOptions<NotifySettings>>();
        settingsOptions.Value.Returns(_settings);


        _sqs.SendMessageAsync(Arg.Do<Envelop>(x => _envelopUsed = x), Arg.Any<string>(), Arg.Any<CancellationToken>());
        
        var service = new PublishService(settingsOptions, _sqs);

        // Act
        service.Publish(_notificationEvent, CancellationToken.None).GetAwaiter().GetResult();
        
    }

    [TestMethod]
    public void ShouldIdIsCorrect() => _envelopUsed.Id.Should().Be(_notificationEvent.NotificationId);

    [TestMethod]
    public void ShouldDataIsCorrect() => _envelopUsed.Data.Should().Be(_notificationEvent.ToData());

    [TestMethod]
    public void ShouldDataContentTypeIsCorrect() => _envelopUsed.DataContentType.Should().Be(_settings.DataContentTypeEvent);

    [TestMethod]
    public void ShouldSourceIsCorrect() => _envelopUsed.Source.Should().Be(_settings.SourceEvent);

    [TestMethod]
    public void ShouldSpecVersionIsCorrect() => _envelopUsed.SpecVersion.Should().Be(_settings.SpecVersionEvent);

    [TestMethod]
    public void ShouldSubjectIsCorrect() => _envelopUsed.Subject.Should().Be(_notificationEvent.Description);

    [TestMethod]
    public void ShouldTypeIsCorrect() => _envelopUsed.Type.Should().Be(_notificationEvent.Type);
}
