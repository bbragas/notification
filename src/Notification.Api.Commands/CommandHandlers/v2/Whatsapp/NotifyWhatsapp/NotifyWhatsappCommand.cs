using MediatR;
using Notification.Api.Messages.Whatsapp.DTOs;

namespace Notification.Api.Commands.CommandHandlers.v2.Whatsapp.NotifyWhatsapp;

public record NotifyWhatsappCommand(
    Guid UnitId, 
    Guid ProjectId, 
    Guid? ProjectEntityId, 
    string ProjectName,  
    string Message,
    MessageType MessageType,
    string? Subtitle,
    string RecipientNumber,
    string? ExternalId,
    DateTime? ScheduledTo) : IRequest<NotifyWhatsappResponseDTO>;

