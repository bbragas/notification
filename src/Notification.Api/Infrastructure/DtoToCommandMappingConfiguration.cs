using AutoMapper;
using Notification.Api.Commands.CommandHandlers.v1.NotifyEmail;
using Notification.Api.Commands.CommandHandlers.v1.NotifySms;
using Notification.Api.Models.v1.Email;
using Notification.Api.Models.v1.Sms;

namespace Notification.Api.Infrastructure;

public class DtoToCommandMappingConfiguration : Profile
{
    public DtoToCommandMappingConfiguration()
    {
        CreateMap<NotifyEmailDTO, NotifyEmailCommand>();
        CreateMap<NotifySmsDTO, NotifySmsCommand>();
    }
}
