using FluentValidation;

namespace Notification.Api.Models.v1.Sms.Validators;

public class NotifySmsDTOValidator : AbstractValidator<NotifySmsDTO>
{
    public NotifySmsDTOValidator()
    {
        RuleFor(dto => dto.Client).NotEmpty();
        RuleFor(dto => dto.Message).NotEmpty();
        RuleFor(dto => dto.Campaign).NotEmpty();
        RuleFor(dto => dto.ProjectId).NotEmpty();
        RuleFor(dto => dto.PhoneNumber).NotEmpty();
        RuleFor(dto => dto.ProjectEntityId).NotEmpty();
    }
}
