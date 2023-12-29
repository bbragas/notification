using AutoFixture;
using Microsoft.Extensions.Logging;
using Notification.Api.Commands.CommandHandlers.v2.Whatsapp.NotifyWhatsapp;
using Notification.Api.Commands.Services;
using Notification.Api.Infrastructure.Messages.v1.WhatsApp;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Messages.Whatsapp.DTOs;
using WhatsappEntity = Notification.Api.Domain.Whatsapp.Whatsapp;


namespace Notification.Api.Commands.Tests.CommandHandlers.v2.Whatsapp.NotifyWhatsapp.WhenHandlingNotifyWhatsappCommandTests;

[TestClass]
public class WithNotifyWhatsappCommandWithMediaBase64Type
{
    private readonly Fixture _fixture = new();
    private readonly IWriteRepository _writeRepository;
    private readonly IPublishService _publishService;
    private readonly NotifyWhatsappCommand _command;

    public WithNotifyWhatsappCommandWithMediaBase64Type()
    {
        // Arrange
        var logger = Substitute.For<ILogger<NotifyWhatsappCommandHandler>>();

        _publishService = Substitute.For<IPublishService>();
        _writeRepository = Substitute.For<IWriteRepository>();

        _command = _fixture.Build<NotifyWhatsappCommand>()
            .With(x => x.MessageType, () => MessageType.Base64Media)
            .Create();

        // Act
        new NotifyWhatsappCommandHandler(_writeRepository, _publishService, logger).Handle(_command, CancellationToken.None).Wait();
    }

    [TestMethod]
    public Task IEnvelopEventServiceShouldPublishAMessageWithCorrectValues()
       => _publishService.Received().Publish(
           Arg.Is<SendMediaBase64WhatsAppEvent>(p => AssertWhatsappBase64MediaEvent(p)),
           Arg.Any<CancellationToken>());

    [TestMethod]
    public async Task IWriteRepositoryShouldNotReceivedSendMessageAsyncWithCorrectValues()
     => await _writeRepository.Received().CreateAsync(
             Arg.Is<WhatsappEntity>(p => AssertWhatsapp(p)),
             Arg.Any<CancellationToken>());

    private bool AssertWhatsappBase64MediaEvent(SendMediaBase64WhatsAppEvent @event)
    {
        @event.NotificationId.Should().NotBeEmpty();
        @event.To.Should().Be(_command.RecipientNumber);
        @event.MediaBase64.Should().Be(_command.Message);
        @event.Subtitle.Should().Be(_command.Subtitle);

        return true;
    }

    private bool AssertWhatsapp(WhatsappEntity entity)
    {
        entity.Id.Should().NotBeEmpty();
        entity.ExternalId.Should().Be(_command.ExternalId);
        entity.UnitId.Should().NotBeEmpty();
        entity.UnitId.Should().Be(_command.UnitId);
        entity.Subtitle.Should().Be(_command.Subtitle);
        entity.Message.Should().Be(_command.Message);
        entity.Project.Should().NotBeNull();
        entity.Project.Id.Should().Be(_command.ProjectId);
        entity.Project.Name.Should().Be(_command.ProjectName);
        entity.Project.EntityId.Should().Be(_command.ProjectEntityId);
        return true;
    }
}
